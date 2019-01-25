
namespace Redux.Primitives
{
    /// <summary>
    /// Represents a primitive Redux reducer.
    /// </summary>
    /// <typeparam name="TState">Type of the state.</typeparam>
    /// <param name="state">StateWrapper before the reduction.</param>
    /// <param name="action">Action used for the reduction.</param>
    /// <returns>StateWrapper after the reduction.</returns>
    public delegate TState Reducer<TState>(TState state, ReduxAction action);
}
