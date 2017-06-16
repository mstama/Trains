using System;
using System.Collections.Generic;
using Trains.Models;

namespace Trains.Interfaces
{
    /// <summary>
    /// Graph walker
    /// </summary>
    public interface IGraphWalker
    {
        /// <summary>
        /// Find all paths between 2 towns
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        IList<string> FindPaths(IGraph graph, string origin, string dest, int limit, PathOption option);

        /// <summary>
        /// Return the shortest path to a town from a given town
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns>Tuple breadcrumb and distance</returns>
        Tuple<string, int> ShortestPath(IGraph graph, string origin, string dest);

        /// <summary>
        /// Find shortest path distance between 2 towns
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        int ShortestPathDistance(IGraph graph, string origin, string dest);

        /// <summary>
        /// Return the shortest paths to all towns from a given town
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <returns>
        /// List of tuples in the format (Town name, Previous Town name, distance from origin)
        /// </returns>
        IList<Tuple<string, string, int>> ShortestPaths(IGraph graph, string origin);

        /// <summary>
        /// Calculate route distance
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        int TotalRouteDistance(IGraph graph, params string[] names);
    }
}