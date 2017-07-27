// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text;
using Trains.Models;

namespace Trains.Services
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

            /// <summary>
            /// Build breadcrumb
            /// </summary>
            /// <returns></returns>
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