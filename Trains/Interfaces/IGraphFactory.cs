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