using System;
using System.Collections.Generic;
using Trains.Interfaces;

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
        public Town(IGraph graph, string name)
        {
            Name = name;
            graph.Towns[name] = this;
        }

        /// <summary>
        /// Name of the town
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Routes from town
        /// </summary>
        public IDictionary<string, Route> Routes { get; } = new Dictionary<string, Route>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Add route to town
        /// </summary>
        /// <param name="route"></param>
        public void AddRoute(Route route)
        {
            Routes[route.Destination.Name] = route;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}