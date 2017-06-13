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

        /// <summary>
        /// Return graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        IGraphQuery RetrieveGraph(IGraph graph);
    }
}