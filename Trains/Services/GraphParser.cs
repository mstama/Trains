using System;
using System.Collections.Generic;
using System.Text;
using Trains.Interfaces;

namespace Trains.Services
{
    public class GraphParser : IGraphParser<string, IGraph>
    {
        private static char[] _separator = new char[] { ',',' '};
        IGraphFactory _factory;

        public GraphParser(IGraphFactory factory)
        {
            _factory = factory;
        }

        public IGraph Parse(string text)
        {
            var graph = _factory.CreateGraph();
            var commands = text.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
            foreach(var command in commands)
            {
                var origin = graph.AddTown(command[0].ToString());
                var destination = graph.AddTown(command[1].ToString());
                int distance = int.Parse(command.Substring(2));
                graph.AddRoute(origin, destination, distance);
            }
            return graph;
        }
    }
}
