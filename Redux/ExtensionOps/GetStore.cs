using System;

namespace Redux
{
    public static partial class ExtensionOps
    {
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
