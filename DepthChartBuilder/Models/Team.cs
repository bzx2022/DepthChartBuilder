using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChartBuilder.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Player> Players { get; set; }
    }
}
