using System;

namespace Redux
{
    public static class Redux
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
            Store<T> realStore = store as Store<T>;

            if (realStore == null)
            {
                throw new InvalidOperationException("Store does not contain the queried type.");
            }

            return realStore.State;
        }
    }
}
