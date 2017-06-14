using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Interfaces
{
    /// <summary>
    /// Queriable graph
    /// </summary>
    public interface IGraphQuery
    {
        /// <summary>
        /// Calculate route distance
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        int CalculateRouteDistance(params string[] names);

        /// <summary>
        /// Find all paths between 2 towns
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        IList<string> FindPaths(string origin, string dest, int limit, PathOption option);

        /// <summary>
        /// Find shortest path distance between 2 towns
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        int ShortestPathDistance(string origin, string dest);
    }
}
