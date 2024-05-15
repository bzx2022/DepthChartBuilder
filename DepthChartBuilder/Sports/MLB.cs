using DepthChartBuilder.Interfaces;
using DepthChartBuilder.Models;

namespace DepthChartBuilder.Sports
{
    public class MLB : ISport
    {
        public int GetMaxDepth()
        {
            return 25;
        }

        public IEnumerable<Position> GetPositions()
        {
            return new List<Position>
            {
                new Position { Name = "Pitcher", Abbreviation = "P" },
                new Position { Name = "Catcher", Abbreviation = "C" },
                new Position { Name = "First Base", Abbreviation = "1B" },
                new Position { Name = "Second Base", Abbreviation = "2B" },
                new Position { Name = "Third Base", Abbreviation = "3B" },
                new Position { Name = "Shortstop", Abbreviation = "SS" },
                new Position { Name = "Left Field", Abbreviation = "LF" },
                new Position { Name = "Center Field", Abbreviation = "CF" },
                new Position { Name = "Right Field", Abbreviation = "RF" },
                new Position { Name = "Designated Hitter", Abbreviation = "DH" }
            };
        }

        public string ValidatePositionAbbreviation(string position)
        {
            return GetPositions().FirstOrDefault(p => p.Abbreviation == position)?.Abbreviation;
        }
    }
}
