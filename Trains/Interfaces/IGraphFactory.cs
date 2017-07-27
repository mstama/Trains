// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Trains.Interfaces
{
    /// <summary>
    /// Graph factory interface
    /// </summary>
    public interface IGraphFactory
    {
        /// <summary>
        /// Create graph
        /// </summary>
        /// <returns></returns>
        IGraph CreateGraph();
    }
}