﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public partial class GraphWalker
    {
        /// <summary>
        /// Processing class to find shortest path
        /// </summary>
        private class ShortTown
        {
            public Town TownData { get; set; }
            public Town Previous { get; set; }
            public int Distance { get; set; }
            public ShortTown(Town dest, Town previous, int distance)
            {
                TownData = dest;
                Previous = previous;
                Distance = distance;
            }
        }
    }
}
