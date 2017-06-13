using System.Collections.Generic;

namespace Trains.Models
{
    /// <summary>
    /// Represents a town
    /// </summary>
    public class Town
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public Town(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Name of the town
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Routes from town
        /// </summary>
        public IList<Route> Routes { get; } = new List<Route>();

        /// <summary>
        /// Add route to town
        /// </summary>
        /// <param name="route"></param>
        public void AddRoute(Route route)
        {
            Routes.Add(route);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}