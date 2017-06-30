namespace Trains.Interfaces
{
    /// <summary>
    /// Parser interface
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IGraphParser<in TSource,out TTarget>
    {
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TTarget Parse(TSource input);
    }
}