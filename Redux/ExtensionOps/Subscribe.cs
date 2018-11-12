using System;

namespace Redux
{
    public static partial class ExtensionOps
    {
        /// <summary>
        /// Subscribes an eventhandler to a store's StateChanged event.
        /// </summary>
        /// <typeparam name="TState">Expected type of the store state.</typeparam>
        /// <param name="store">Store that is being subscribed.</param>
        /// <param name="handler">A function that will be invoked by the event after subscription.</param>
        /// <returns>A function that can be called in order to unsubscribe from then event.</returns>
        public static Action Subscribe<TState>(this IStore store, StateChangedEventHandler<TState> handler)
        {
            Store<TState> realStore = store.GetStore<TState>();
            realStore.StateChanged += handler;
            bool subscribed = true;

            return () =>
            {
                if (subscribed)
                {
                    realStore.StateChanged -= handler;
                    subscribed = false;
                }
            };
        }
    }
}
