﻿using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Interfaces
{
    /// <summary>
    /// Graph walker
    /// </summary>
    public interface IGraphWalker
    {
        /// <summary>
        /// Calculate route distance
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        int TotalRouteDistance(IGraph graph, params string[] names);

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
        /// Find shortest path distance between 2 towns
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        int ShortestPathDistance(IGraph graph, string origin, string dest);

        IList<Tuple<string, string, int>> ShortestPaths(IGraph graph, string origin);

        Tuple<string, int> ShortestPath(IGraph graph, string origin, string dest);
    }
}
