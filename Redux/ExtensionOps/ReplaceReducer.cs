
namespace Redux
{
    public static partial class ExtensionOps
    {
        /// <summary>
        /// Replaces the reducer that is currently being used by the store.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="store"></param>
        /// <param name="nextReducer">New reducer to replace the old one.</param>
        public static void ReplaceReducer<TState>(this IStore store, Reducer<TState> nextReducer)
        {
            store.GetStore<TState>().ReplaceReducer(nextReducer);
        }
    }
}
