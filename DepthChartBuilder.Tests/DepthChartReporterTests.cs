using DepthChartBuilder.Interfaces;
using DepthChartBuilder.Models;
using Moq;

namespace DepthChartBuilder.Tests
{
    [TestFixture]
    public class DepthChartReporterTests
    {
        private Mock<IDepthChartManager> _depthChartManagerMock;
        private DepthChartReporter _depthChartReporter;

        [SetUp]
        public void SetUp()
        {
            _depthChartManagerMock = new Mock<IDepthChartManager>();
            _depthChartReporter = new DepthChartReporter(_depthChartManagerMock.Object);
        }

        [Test]
        public void GetFullDepthChart_ReturnsCorrectString()
        {
            var player = new Player { Number = 1, Name = "John Doe" };
            string position = "Quarterback";
            var playerDepth = new PlayerDepth { Player = player, Position = position, Depth = 0 };
            _depthChartManagerMock.Setup(dcm => dcm.GetDepthChartData()).Returns(new List<PlayerDepth> { playerDepth });

            string fullDepthChart = _depthChartReporter.GetFullDepthChart();

            Assert.That(fullDepthChart, Is.EqualTo($"{position} - (#{player.Number}, {player.Name})"));
        }

        // ... other tests ...
    }
}
