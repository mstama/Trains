// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Trains.Interfaces;

namespace Trains.Services
{
    /// <summary>
    /// Parse a graph in the format AB5 Where town is represented with a single letter
    /// </summary>
    public class GraphParser : IGraphParser<string, IGraph>
    {
        private static readonly char[] _separator = { ',', ' ' };
        private readonly IGraphFactory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory"></param>
        public GraphParser(IGraphFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Parse text and returna a graph
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IGraph Parse(string input)
        {
            var graph = _factory.CreateGraph();
            foreach (var command in input.Split(_separator, StringSplitOptions.RemoveEmptyEntries))
            {
                graph.AddRoute(command[0].ToString(), command[1].ToString(), int.Parse(command.Substring(2)));
            }
            return graph;
        }
    }
}