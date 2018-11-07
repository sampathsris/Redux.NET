﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux
{
    public static class ExtensionOps
    {
        /// <summary>
        /// Gets the state of a store.
        /// </summary>
        /// <typeparam name="T">Expected type of the store state.</typeparam>
        /// <param name="store">Store that is being queried.</param>
        /// <returns>Current state of the store.</returns>
        public static T GetState<T>(this IStore store)
        {
            return Ops.GetStore<T>(store).State;
        }

        /// <summary>
        /// Subscribes an eventhandler to a store's StateChanged event.
        /// </summary>
        /// <typeparam name="T">Expected type of the store state.</typeparam>
        /// <param name="store">Store that is being subscribed.</param>
        /// <param name="handler">A function that will be invoked by the event after subscription.</param>
        /// <returns>A function that can be called in order to unsubscribe from then event.</returns>
        public static System.Action Subscribe<T>(this IStore store, StateChangedEventHandler<T> handler)
        {
            Store<T> realStore = Ops.GetStore<T>(store);
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
