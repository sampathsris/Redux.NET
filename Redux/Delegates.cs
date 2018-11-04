
namespace Redux
{
    /// <summary>
    /// A function that is a Redux reducer.
    /// </summary>
    /// <typeparam name="T">Type of the reduced state.</typeparam>
    /// <param name="state">State before the reduction.</param>
    /// <param name="action">Action used for the reduction.</param>
    /// <returns>State after the reduction.</returns>
    public delegate T Reducer<T>(T state, Action action);

    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <typeparam name="T">Type of the state.</typeparam>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    internal delegate void StateChangedEventHandler<T>(
        IStore store,
        T state
    );
}
