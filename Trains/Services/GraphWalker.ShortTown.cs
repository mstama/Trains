using Trains.Models;

namespace Trains.Services
{
    public partial class GraphWalker
    {
        /// <summary>
        /// Processing class to find shortest path
        /// </summary>
        private class ShortTown
        {
            public Town Data { get; }

            public int Distance { get; set; }

            public Town Previous { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="dest"></param>
            /// <param name="previous"></param>
            /// <param name="distance"></param>
            public ShortTown(Town dest, Town previous, int distance)
            {
                Data = dest;
                Previous = previous;
                Distance = distance;
            }
        }
    }
}