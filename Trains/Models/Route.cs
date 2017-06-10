using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public class Route
    {
        public Town Destination { get; set; }
        public int Distance { get; set; }

        public Route(Town origin, Town destination, int distance)
        {
            Destination = destination;
            Distance = distance;
            origin.AddRoute(this);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}",Destination,Distance);
        }
    }
}
