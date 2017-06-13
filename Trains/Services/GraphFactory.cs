using System;
using System.Collections.Generic;
using System.Text;
using Trains.Interfaces;
using Trains.Models;

namespace Trains.Services
{
    public class GraphFactory : IGraphFactory
    {
        public IGraph CreateGraph()
        {
            return new Graph();
        }

        public IGraphQuery RetrieveGraph(IGraph graph)
        {
            return (IGraphQuery)graph;
        }
    }
}
