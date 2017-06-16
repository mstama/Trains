using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public partial class GraphWalker
    {
        /// <summary>
        /// Processing class to find all paths
        /// </summary>
        private class MetaTown
        {
            public Town TownData { get; set; }
            public int Stops { get; set; }
            public string Breadcrumb { get; set; }
            public int TotalDistance { get; set; }

            public MetaTown(Town town, int stops, string breadcrumb, int distance)
            {
                TownData = town;
                Stops = stops;
                Breadcrumb = string.Format("{0}{1}", breadcrumb, town.Name);
                TotalDistance = distance;
            }

            public MetaTown(Town town, int stops)
            {
                TownData = town;
                Stops = stops;
                Breadcrumb = town.Name;
                TotalDistance = 0;
            }
        }
    }
}
