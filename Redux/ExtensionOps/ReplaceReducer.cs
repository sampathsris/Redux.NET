
namespace Redux
{
    public static partial class ExtensionOps
    {
        /// <summary>
        /// Replaces the reducer that is currently being used by the store.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="nextReducer">New reducer to replace the old one.</param>
        public static void ReplaceReducer(this IStore store, Reducer nextReducer)
        {
            store.GetStore().ReplaceReducer(nextReducer);
        }
    }
}
