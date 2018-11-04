
namespace Redux
{
    public interface IStore
    {
        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        void Dispatch(Action action);
    }
}
