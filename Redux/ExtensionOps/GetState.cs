
namespace Redux
{
    public static partial class ExtensionOps
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
    }
}
