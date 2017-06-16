using System;
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
            public Town Data { get; }

            public int Distance { get; }

            public int Stops { get; }

            protected MetaTown Previous { get; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="route"></param>
            /// <param name="previous"></param>
            public MetaTown(Route route, MetaTown previous)
            {
                Data = route.Destination;
                Stops = previous.Stops + 1;
                Previous = previous;
                Distance = previous.Distance + route.Distance;
            }

            /// <summary>
            /// Constructor First Town
            /// </summary>
            /// <param name="town"></param>
            /// <param name="stops"></param>
            public MetaTown(Town town, int stops)
            {
                Data = town;
                Stops = stops;
                Distance = 0;
            }

            public string Breadcrumb()
            {
                StringBuilder bread = new StringBuilder();
                MetaTown current = this;
                while (current != null)
                {
                    bread.Insert(0, current.Data.Name);
                    current = current.Previous;
                }
                return bread.ToString();
            }
        }
    }
}