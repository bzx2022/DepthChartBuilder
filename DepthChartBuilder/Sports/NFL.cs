using DepthChartBuilder.Interfaces;
using DepthChartBuilder.Models;

namespace DepthChartBuilder.Sports
{
    public class NFL : ISport
    {
        public int GetMaxDepth()
        {
            return 11;
        }

        public IEnumerable<Position> GetPositions()
        {
            return new List<Position>
            {
                new Position { Name = "Quarterback", Abbreviation = "QB" },
                new Position { Name = "Running Back", Abbreviation = "RB" },
                new Position { Name = "Tight End", Abbreviation = "TE" },
                new Position { Name = "Linebacker", Abbreviation = "LB" },
                new Position { Name = "Right Cornerback", Abbreviation = "RCB" },
                new Position { Name = "Left Cornerback", Abbreviation = "LCB" },
                new Position { Name = "Strong Safety", Abbreviation = "SS" },
                new Position { Name = "Free Safety", Abbreviation = "FS" },
                new Position { Name = "Defensive Tackle", Abbreviation = "DT" },
                new Position { Name = "Defensive End", Abbreviation = "DE" },
                new Position { Name = "Kicker", Abbreviation = "K" },
                new Position { Name = "Punter", Abbreviation = "PT" },
                new Position { Name = "Long Snapper", Abbreviation = "LS" },
                new Position { Name = "Right Wide Receiver", Abbreviation = "RWR" },
                new Position { Name = "Left Wide Receiver", Abbreviation = "LWR" },
                new Position { Name = "Slot Wide Receiver", Abbreviation = "SWR"},
                new Position { Name = "Center", Abbreviation = "C" },
                new Position { Name = "Left Tackle", Abbreviation = "LT" },
                new Position { Name = "Right Tackle", Abbreviation = "RT" },
                new Position { Name = "Left Guard", Abbreviation = "LG" },
                new Position { Name = "Right Guard", Abbreviation = "RG" },
                new Position { Name = "Fullback", Abbreviation = "FB" },
                new Position { Name = "H-Back", Abbreviation = "HB" },
                new Position { Name = "Nose Tackle", Abbreviation = "NT" },
                new Position { Name = "Slot Receiver", Abbreviation = "SR" },
                new Position { Name = "Left Defensive End", Abbreviation = "LDE" },
                new Position { Name = "Right Defensive End", Abbreviation = "RDE" },
                new Position { Name = "Will Linebacker", Abbreviation = "WLB" },
                new Position { Name = "Mike Linebacker", Abbreviation = "MLB" },
                new Position { Name = "Nickleback", Abbreviation = "NB" },
                new Position { Name = "Placekicker", Abbreviation = "PK" },
                new Position { Name = "Holder", Abbreviation = "H" },
                new Position { Name = "Kickoff Specialist", Abbreviation = "KO" },
                new Position { Name = "Punt Returner", Abbreviation = "PR" },
                new Position { Name = "Kick Returner", Abbreviation = "KR" },
                new Position { Name = "Place Kicker", Abbreviation = "PK" },
            };
        }

        public string ValidatePositionAbbreviation(string position)
        {
            return GetPositions().FirstOrDefault(p => p.Abbreviation == position)?.Abbreviation;
        }
    }
}
