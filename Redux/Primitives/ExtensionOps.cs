using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Primitives
{
    public static class ExtensionOps
    {
        public static Action Subscribe<TState>(this IStore store, StateChangedEventHandler<TState> handler) where TState : struct
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

        internal static Store<TState> GetStore<TState>(this IStore store) where TState : struct
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
