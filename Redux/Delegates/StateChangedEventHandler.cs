
namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <typeparam name="TState">Type of the state.</typeparam>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    public delegate void StateChangedEventHandler<TState>(
        IStore store,
        TState state
    );
}
