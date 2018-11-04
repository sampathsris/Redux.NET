using System;

namespace Redux
{
    public static class Ops
    {
        /// <summary>
        /// Creates a Redux store that serves a given type T.
        /// </summary>
        /// <typeparam name="T">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<T>(Reducer<T> reducer)
        {
            return new Store<T>(reducer);
        }

        /// <summary>
        /// Gets the state of a store.
        /// </summary>
        /// <typeparam name="T">Expected type of the store state.</typeparam>
        /// <param name="store">Store that is being queried.</param>
        /// <returns>Current state of the store.</returns>
        public static T GetState<T>(IStore store)
        {
            return GetStore<T>(store).State;
        }

        /// <summary>
        /// Subscribes an eventhandler to a store's StateChanged event.
        /// </summary>
        /// <typeparam name="T">Expected type of the store state.</typeparam>
        /// <param name="store">Store that is being subscribed.</param>
        /// <param name="handler">A function that will be invoked by the event after subscription.</param>
        /// <returns>A function that can be called in order to unsubscribe from then event.</returns>
        public static System.Action Subscribe<T>(IStore store, StateChangedEventHandler<T> handler)
        {
            Store<T> realStore = GetStore<T>(store);
            realStore.StateChanged += handler;
            bool subscribed = true;

            return () => {
                if (subscribed)
                {
                    realStore.StateChanged -= handler;
                    subscribed = false;
                }
            };
        }

        private static Store<T> GetStore<T>(IStore store)
        {
            Store<T> realStore = store as Store<T>;

            if (realStore == null)
            {
                throw new InvalidOperationException("Store does not contain the queried type.");
            }
            return realStore;
        }
    }
}
