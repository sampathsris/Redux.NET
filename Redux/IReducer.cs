
namespace Redux
{
    /// <summary>
    /// Provides the interface for a Redux reducer.
    /// </summary>
    /// <typeparam name="TState">Type of the state tree.</typeparam>
    public interface IReducer<TState>
    {
        /// <summary>
        /// Represents a Redux reducer.
        /// </summary>
        /// <param name="state">State before the reduction.</param>
        /// <param name="action">Action used for the reduction.</param>
        /// <returns>State after the reduction.</returns>
        TState Reduce(TState state, ReduxAction action);
    }
}
