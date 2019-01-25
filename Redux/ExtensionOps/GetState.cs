
namespace Redux
{
    public static partial class ExtensionOps
    {
        /// <summary>
        /// Gets the state of a store.
        /// </summary>
        /// <param name="store">Store that is being queried.</param>
        /// <returns>Current state of the store.</returns>
        public static IState GetState(this IStore store)
        {
            return store.GetStore().GetState();
        }
    }
}
