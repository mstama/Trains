using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public class Route
    {
        public Town Origin { get; set; }
        public Town Destination { get; set; }
        public int Distance { get; set; }

        public Route(Town origin, Town destination, int distance)
        {
            Origin = origin;
            Destination = destination;
            Distance = distance;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}",Origin,Destination,Distance);
        }
    }
}
