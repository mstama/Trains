using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public class MetaTown
    {
        public Town TownData { get; set; }
        public int Depth { get; set; }
        public string Breadcrumb { get; set; }

        public MetaTown(Town town, int depth, string breadcrumb)
        {
            TownData = town;
            Depth = depth;
            Breadcrumb = string.Format("{0}{1}",breadcrumb, town.Name);
        }

        public MetaTown(Town town, int depth)
        {
            TownData = town;
            Depth = depth;
            Breadcrumb = town.Name;
        }
    }
}
