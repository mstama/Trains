namespace Trains.Models
{
    /// <summary>
    /// Represents a route
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="distance"></param>
        public Route(Town origin, Town destination, int distance)
        {
            Destination = destination;
            Distance = distance;
            origin.AddRoute(this);
        }

        /// <summary>
        /// Destination
        /// </summary>
        public Town Destination { get; }

        /// <summary>
        /// Distance
        /// </summary>
        public int Distance { get; }

        public override string ToString()
        {
            return string.Format("{0}{1}", Destination, Distance);
        }
    }
}