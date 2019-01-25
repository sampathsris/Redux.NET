﻿
namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    public delegate void StateChangedEventHandler(
        IStore store,
        IState state
    );
}
