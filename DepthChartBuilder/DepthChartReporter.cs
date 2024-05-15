using DepthChartBuilder.Interfaces;
using System.Text;

namespace DepthChartBuilder
{
    public class DepthChartReporter : IDepthChartReporter
    {
        private readonly IDepthChartManager _depthChartManager;
        public DepthChartReporter(IDepthChartManager depthChartManager)
        {
            _depthChartManager = depthChartManager;
        }

        public string GetFullDepthChart()
        {
            var depthChartData = _depthChartManager.GetDepthChartData();
            if (!depthChartData.Any())
            {
                throw new NullReferenceException("There are no players in the depth chart");
            }

            var depthChart = new StringBuilder();
            foreach (var position in depthChartData.GroupBy(p => p.Position))
            {
                depthChart.Append($"{position.Key} -");
                foreach (var player in position.OrderBy(p => p.Depth))
                {
                    depthChart.Append($" (#{player.Player.Number}, {player.Player.Name}),");
                }

                depthChart.Remove(depthChart.Length - 1, 1).AppendLine();
            }

            if (depthChart.Length > 0)
            {
                depthChart.Remove(depthChart.Length - 2, 2);
            }

            return depthChart.ToString();
        }
    }
}
