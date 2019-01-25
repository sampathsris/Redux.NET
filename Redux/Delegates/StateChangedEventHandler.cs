
namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <param name="sender">The store that publishes the event.</param>
    /// <param name="state">StateWrapper of the store that publishes the event.</param>
    public delegate void StateChangedEventHandler(
        object sender,
        IState state
    );
}
