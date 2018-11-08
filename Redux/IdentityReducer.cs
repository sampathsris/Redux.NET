
namespace Redux
{
    /// <summary>
    /// A reducer that has the identity function for the reducer
    /// function. In other words, it simply returns the previous state.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IdentityReducer<T> : IReducer<T>
    {
        public T Reduce(T state, ReduxAction action)
        {
            return state;
        }
    }
}
