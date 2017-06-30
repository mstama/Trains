using System;
using Trains.Interfaces;

namespace Trains.Services
{
    /// <summary>
    /// Parse a graph in the format AB5 Where town is represented with a single letter
    /// </summary>
    public class GraphParser : IGraphParser<string, IGraph>
    {
        private static char[] _separator = { ',', ' ' };
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
        /// <param name="text"></param>
        /// <returns></returns>
        public IGraph Parse(string text)
        {
            var graph = _factory.CreateGraph();
            var commands = text.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var command in commands)
            {
                graph.AddRoute(command[0].ToString(), command[1].ToString(), int.Parse(command.Substring(2)));
            }
            return graph;
        }
    }
}