using DepthChartBuilder.Interfaces;
using DepthChartBuilder.Models;
using DepthChartBuilder.Sports;

namespace DepthChartBuilder
{
    public class DepthChartManager : IDepthChartManager
    {
        private readonly ISport? _sport;
        private List<PlayerDepth> _depthChart;
        private Team? _team;

        public DepthChartManager(Team? team = null, ISport? sport = null)
        {
            _depthChart = new List<PlayerDepth>();
            _team = team;
            _sport = sport ?? new NFL();
        }

        public void AddPlayerToDepthChart(Player player, string position, int? depth)
        {
            ValidatePlayerAndPosition(player, position);

            var endPosition = depth ?? GetEndPosition(position);

            if (endPosition >= _sport.GetMaxDepth())
            {
                throw new InvalidOperationException($"Position {position} is full");
            }

            if (depth.HasValue && (depth < 0 || depth >= _sport.GetMaxDepth()))
            {
                throw new InvalidOperationException($"Depth must be between 0 and {_sport.GetMaxDepth() - 1}");
            }

            var existingPlayer = _depthChart.FirstOrDefault(p => p.Player.Number == player.Number && p.Position == position);
            if (existingPlayer != null)
            {
                throw new InvalidOperationException($"Player {player.Name} already exists in the depth chart at the position: {position}");
            }

            IncrementPlayerDepths(position, depth);

            _depthChart.Add(new PlayerDepth { Player = player, Position = position, Depth = endPosition });
        }

        public List<Player> RemovePlayerFromDepthChart(string position, Player player)
        {
            ValidatePlayerAndPosition(player, position);

            var playersToRemove = _depthChart.Where(p => p.Player.Number == player.Number && p.Position == position).ToList();
            foreach (var p in playersToRemove)
            {
                _depthChart.Remove(p);
                var playersAtDepth = _depthChart.Where(p => p.Position == position && p.Depth > p.Depth).ToList();

                foreach (var playerAtDepth in playersAtDepth)
                {
                    playerAtDepth.Depth--;
                }
            }

            return playersToRemove.Select(p => p.Player).ToList();
        }

        public List<Player> GetBackups(string position, Player player)
        {
            ValidatePlayerAndPosition(player, position);

            var playerDepth = _depthChart.FirstOrDefault(p => p.Player.Number == player.Number && p.Position == position);
            if (playerDepth == null)
            {
                return new List<Player>();
            }

            return _depthChart.Where(p => p.Position == position && p.Depth > playerDepth.Depth).Select(p => p.Player).ToList();
        }


        public List<PlayerDepth> GetDepthChartData()
        {
            return _depthChart;
        }

        private void ValidatePlayerAndPosition(Player player, string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                throw new ArgumentNullException("Position is required", nameof(position));
            }

            if (player == null || string.IsNullOrWhiteSpace(player.Name))
            {
                throw new ArgumentNullException(nameof(player), "A valid Player object must be provided.");
            }

            if (_sport.ValidatePositionAbbreviation(position) is null)
            {
                throw new ArgumentException($"Position {position} is not valid", nameof(position));
            }

            if (_team != null && !_team.Players.Any(p => p.Number == player.Number && p.Name == player.Name))
            {
                throw new ArgumentException($"Player {player.Name} is not on the team roster", nameof(player));
            }
        }

        private int GetEndPosition(string position)
        {
            var playersInPosition = _depthChart.Where(p => p.Position == position).ToList();
            return playersInPosition.Any() ? playersInPosition.Max(p => p.Depth) + 1 : 0;
        }

        private void IncrementPlayerDepths(string position, int? depth)
        {
            var playersAtDepth = _depthChart.Where(p => p.Position == position && p.Depth >= depth).ToList();
            foreach (var p in playersAtDepth)
            {
                p.Depth++;
            }
        }
    }    
}