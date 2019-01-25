
namespace Redux.Primitives
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a primitive store.
    /// </summary>
    /// <param name="sender">The store that publishes the event.</param>
    /// <param name="state">StateWrapper of the store that publishes the event.</param>
    /// <typeparam name="TState">Type of the primitive value</typeparam>
    public delegate void StateChangedEventHandler<TState>(
        object sender,
        TState state
    );
}
