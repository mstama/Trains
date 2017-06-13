namespace Trains.Interfaces
{
    /// <summary>
    /// Parser interface
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IGraphParser<TSource, TTarget>
    {
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TTarget Parse(TSource input);
    }
}