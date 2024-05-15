using DepthChartBuilder.Models;

namespace DepthChartBuilder.Tests
{
    [TestFixture]
    public class DepthChartManagerTests
    {
        private DepthChartManager _depthChartManager;
        private Player _defaultPlayer;
        private string _defaultPosition;

        [SetUp]
        public void SetUp()
        {
            _depthChartManager = new DepthChartManager();
            _defaultPlayer = new Player { Number = 1, Name = "John Doe" };
            _defaultPosition = "QB";
        }

        [Test]
        public void AddingPlayerToDepthChart_ShouldSucceed()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, null);

            Assert.That(_depthChartManager.GetDepthChartData(), Has.Exactly(1).Matches<PlayerDepth>(pd => pd.Player == _defaultPlayer && pd.Position == _defaultPosition));
        }

        [Test]
        public void AddingNullPlayerToDepthChart_ShouldFail()
        {
            Player invalidPlayer = null;
            Assert.Throws<ArgumentNullException>(() => _depthChartManager.AddPlayerToDepthChart(invalidPlayer, _defaultPosition, null));
        }

        [Test]
        public void AddingPlayerWithNullDetailsToDepthChart_ShouldFail()
        {
            Player invalidPlayer = new Player();
            Assert.Throws<ArgumentNullException>(() => _depthChartManager.AddPlayerToDepthChart(invalidPlayer, _defaultPosition, null));
        }

        [Test]
        public void AddingNullPositionToDepthChart_ShouldFail()
        {
            string invalidPosition = null;
            Assert.Throws<ArgumentNullException>(() => _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, invalidPosition, null));
        }

        [Test]
        public void AddingNonTeamPlayerToDepthChart_ShouldFail()
        {
            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            var team = new Team
            {
                Name = "Tampa Bay Buccaneers",
                Location = "Tampa, FL",
                Players = new List<Player> { player2 }
            };
            _depthChartManager = new DepthChartManager(team);

            Assert.Throws<ArgumentException>(() => _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, null));
        }

        [Test]
        public void AddingPlayerAtSpecificPosition_ShouldShiftExistingPlayersDown()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            _depthChartManager.AddPlayerToDepthChart(player2, _defaultPosition, 0);

            Assert.That(_depthChartManager.GetDepthChartData(), Has.Exactly(1).Matches<PlayerDepth>(pd => pd.Player == player2 && pd.Position == _defaultPosition && pd.Depth == 0));
        }

        [Test]
        public void AddingSamePlayerMultipleTimesToSamePosition_ShouldFail()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            Assert.Throws<InvalidOperationException>(() => _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 1));
        }

        [Test]
        public void AddingSamePlayerMultipleTimesToDifferentPosition_ShouldSucceed()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, "LWR", 0);

            Assert.That(_depthChartManager.GetDepthChartData(), Has.Exactly(2).Matches<PlayerDepth>(pd => pd.Player == _defaultPlayer));
        }

        [Test]
        public void AddingPlayerToDepthChartWithInvalidPosition_ShouldFail()
        {
            string position = "WQB";

            Assert.Throws<ArgumentException>(() => _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, position, 0));
        }
        
        [Test]
        public void AddingPlayerToDepthChartWithInvalidDepth_ShouldFail()
        {
            Assert.Throws<InvalidOperationException>(() => _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, -1));
        }
        
        [Test]
        public void AddingPlayerBeyondMaxDepth_ShouldFail()
        {
            Assert.Throws<InvalidOperationException>(() => _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 100));
        }

        [Test]
        public void RemovePlayerFromDepthChart_ShouldRemovePlayer()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);
            _depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer);

            Assert.That(_depthChartManager.GetDepthChartData(), Has.None.Matches<PlayerDepth>(pd => pd.Player == _defaultPlayer && pd.Position == _defaultPosition));
        }

        [Test]
        public void RemovePlayerFromDepthChart_ShouldReturnTheRemovedPlayer()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer), Has.Exactly(1).Matches<Player>(p => p == _defaultPlayer));
        }

        [Test]
        public void RemovePlayerFromDepthChartThatIsNotInTheProvidedTeam_ShouldFail()
        {
            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            var team = new Team
            {
                Name = "Tampa Bay Buccaneers",
                Location = "Tampa, FL",
                Players = new List<Player> { player2 }
            };
            _depthChartManager = new DepthChartManager(team);
            _depthChartManager.AddPlayerToDepthChart(player2, _defaultPosition, 0);

            Assert.Throws<ArgumentException>(() => _depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer));
        }

        [Test]
        public void RemovePlayerFromDepthChart_ShouldReturnEmptyListIfPlayerNotInDepthChart()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            var player3 = new Player { Number = 3, Name = "Jack Doe" };
            string position2 = "LWR";
            _depthChartManager.AddPlayerToDepthChart(player3, position2, 0);

            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, player3), Is.Empty);
        }

        [Test]
        public void RemovingExistingPlayer_ShouldSucceed()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            _depthChartManager.AddPlayerToDepthChart(player2, _defaultPosition, 1);

            _depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, player2);

            Assert.That(_depthChartManager.GetDepthChartData(), Has.Exactly(1).Matches<PlayerDepth>(pd => pd.Player == _defaultPlayer && pd.Position == _defaultPosition));
        }

        [Test]
        public void RemovingSamePlayerMultipleTimes_ShouldSucceedFirstTimeAndReturnEmptyListThereAfter()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer), Has.Exactly(1).Matches<Player>(p => p == _defaultPlayer));
            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer), Is.Empty);
        }

        [Test]
        public void RemovingPlayerNotAtSpecifiedPosition_ShouldReturnEmptyList()
        {
            string altPosition = "LWR";
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(altPosition, _defaultPlayer), Is.Empty);
        }

        [Test]
        public void RemovingNonExistentPlayer_ShouldReturnEmptyList()
        {
            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer), Is.Empty);
        }

        [Test]
        public void GetBackups_ShouldReturnCorrectBackups()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            _depthChartManager.AddPlayerToDepthChart(player2, _defaultPosition, 1);

            var player3 = new Player { Number = 3, Name = "Jack Doe" };
            _depthChartManager.AddPlayerToDepthChart(player3, _defaultPosition, 2);

            Assert.That(_depthChartManager.GetBackups(_defaultPosition, _defaultPlayer), Has.Exactly(2).Matches<Player>(p => p == player2 || p == player3));
        }

        [Test]
        public void GetBackups_ShouldReturnEmptyListIfPlayerNotInDepthChart()
        {
            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            _depthChartManager.AddPlayerToDepthChart(player2, _defaultPosition, 0);

            Assert.That(_depthChartManager.GetBackups(_defaultPosition, _defaultPlayer), Is.Empty);
        }

        [Test]
        public void GetBackups_ShouldReturnEmptyListIfPlayerNotAtSpecifiedPosition()
        {
            _depthChartManager.AddPlayerToDepthChart(_defaultPlayer, _defaultPosition, 0);

            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            _depthChartManager.AddPlayerToDepthChart(player2, "LWR", 0);

            Assert.That(_depthChartManager.GetBackups(_defaultPosition, _defaultPlayer), Is.Empty);
        }

        [Test]
        public void GetBackups_ShouldReturnEmptyListIfPlayerAtSpecifiedPositionIsNotInDepthChart()
        {
            var player2 = new Player { Number = 2, Name = "Jane Doe" };
            _depthChartManager.AddPlayerToDepthChart(player2, _defaultPosition, 0);

            Assert.That(_depthChartManager.GetBackups(_defaultPosition, _defaultPlayer), Is.Empty);
        }

        [Test]
        public void AddingPlayerToFullPosition_ShouldFail()
        {
            for (int i = 0; i < 11; i++)
            {
                _depthChartManager.AddPlayerToDepthChart(new Player { Number = i, Name = $"Player {i}" }, _defaultPosition, i);
            }

            Assert.Throws<InvalidOperationException>(() => _depthChartManager.AddPlayerToDepthChart(new Player { Number = 111, Name = "Player 111" }, _defaultPosition, null));
        }

        [Test]
        public void RemovingPlayerFromEmptyDepthChart_ShouldReturnEmptyList()
        {
            Assert.That(_depthChartManager.RemovePlayerFromDepthChart(_defaultPosition, _defaultPlayer), Is.Empty);
        }

    }
}