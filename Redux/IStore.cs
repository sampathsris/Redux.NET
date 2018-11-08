
namespace Redux
{
    /// <summary>
    /// Represents a Redux store.
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        void Dispatch(ReduxAction action);
    }
}
