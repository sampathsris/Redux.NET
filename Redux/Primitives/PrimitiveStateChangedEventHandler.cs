
namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a primitive store.
    /// </summary>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    /// <typeparam name="TState">Type of the primitive value</typeparam>
    public delegate void PrimitiveStateChangedEventHandler<TState>(
        IStore store,
        TState state
    );
}
