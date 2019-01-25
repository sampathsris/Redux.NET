using System;

namespace Redux
{
    public static partial class ExtensionOps
    {
        internal static Store GetStore(this IStore store)
        {
            Store realStore = store as Store;

            if (realStore == null)
            {
                throw new InvalidOperationException(Properties.Resources.STORE_TYPEPARAM_INCORRECT_ERROR);
            }

            return realStore;
        }

        internal static PrimitiveStore<TState> GetStore<TState>(this IStore store) where TState: struct
        {
            PrimitiveStore<TState> realStore = store as PrimitiveStore<TState>;

            if (realStore == null)
            {
                throw new InvalidOperationException(Properties.Resources.STORE_TYPEPARAM_INCORRECT_ERROR);
            }

            return realStore;
        }
    }
}
