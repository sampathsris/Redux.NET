
namespace Redux
{
    /// <summary>
    /// Provides the interface for a Redux reducer.
    /// </summary>
    /// <typeparam name="T">Type of the state tree.</typeparam>
    public interface IReducer<T>
    {
        /// <summary>
        /// Represents a Redux reducer.
        /// </summary>
        /// <param name="state">State before the reduction.</param>
        /// <param name="action">Action used for the reduction.</param>
        /// <returns>State after the reduction.</returns>
        T Reduce(T state, Action action);
    }
}
