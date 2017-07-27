// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Trains.Models;

namespace Trains.Interfaces
{
    /// <summary>
    /// Editable graph
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// List of towns
        /// </summary>
        IDictionary<string, Town> Towns { get; }

        /// <summary>
        /// Add a route to graph
        /// </summary>
        /// <remarks>If origin and destination does not exist, it should create it</remarks>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        Route AddRoute(string origin, string dest, int distance);

        /// <summary>
        /// Add a town to graph
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Town AddTown(string name);
    }
}