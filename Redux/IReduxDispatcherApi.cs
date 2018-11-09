using System;

namespace Redux
{
    /// <summary>
    /// An interface that defines Dispatcher and GetState properties.
    /// </summary>
    /// <typeparam name="TState">Type of the target store.</typeparam>
    public interface IReduxDispatcherApi<TState>
    {
        /// <summary>
        /// The dispatcher that is invoked from the IStore.Dispatch implementation.
        /// </summary>
        // both get and set needs to be protected, because if get was public,
        // anyone can invoke the dispatcher.
        Action<ReduxAction> Dispatcher { get; set; }

        /// <summary>
        /// When invoked, returns the current state of the store.
        /// </summary>
        // setter needs to be protected, because only subclasses should be able to
        // replace it.
        Func<TState> GetState { get; set; }
    }
}
