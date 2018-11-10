using System;

namespace Redux
{
    public static class ExtensionOps
    {
        /// <summary>
        /// Gets the state of a store.
        /// </summary>
        /// <typeparam name="TState">Expected type of the store state.</typeparam>
        /// <param name="store">Store that is being queried.</param>
        /// <returns>Current state of the store.</returns>
        public static TState GetState<TState>(this IStore store)
        {
            return store.GetStore<TState>().GetState();
        }

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

        internal static Store<TState> GetStore<TState>(this IStore store)
        {
            Store<TState> realStore = store as Store<TState>;

            if (realStore == null)
            {
                throw new InvalidOperationException(Properties.Resources.STORE_TYPEPARAM_INCORRECT_ERROR);
            }

            return realStore;
        }
    }
}
