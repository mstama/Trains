using Trains.Interfaces;
using Trains.Models;

namespace Trains.Services
{
    /// <summary>
    /// Returns graphs
    /// </summary>
    public class GraphFactory : IGraphFactory
    {
        /// <summary>
        /// Provides a editable graph
        /// </summary>
        /// <returns></returns>
        public IGraph CreateGraph()
        {
            return new Graph();
        }

        /// <summary>
        /// Provides a queriable graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public IGraphWalker RetrieveGraph(IGraph graph)
        {
            return (IGraphWalker)graph;
        }
    }
}