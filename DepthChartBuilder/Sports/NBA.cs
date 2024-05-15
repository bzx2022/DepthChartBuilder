using DepthChartBuilder.Interfaces;
using DepthChartBuilder.Models;

namespace DepthChartBuilder.Sports
{
    public class NBA : ISport
    {
        public int GetMaxDepth()
        {
            return 13;
        }

        public IEnumerable<Position> GetPositions()
        {
            return new List<Position>
            {
                new Position { Name = "Point Guard", Abbreviation = "PG" },
                new Position { Name = "Shooting Guard", Abbreviation = "SG" },
                new Position { Name = "Small Forward", Abbreviation = "SF" },
                new Position { Name = "Power Forward", Abbreviation = "PF" },
                new Position { Name = "Center", Abbreviation = "C" }
            };
        }

        public string ValidatePositionAbbreviation(string position)
        {
            return GetPositions().FirstOrDefault(p => p.Abbreviation == position)?.Abbreviation;
        }
    }
}
