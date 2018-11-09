
namespace Redux
{
    /// <summary>
    /// A reducer that has the identity function for the reducer
    /// function. In other words, it simply returns the previous state.
    /// </summary>
    /// <typeparam name="TState">Type of the reduced state.</typeparam>
    public class IdentityReducer<TState> : IReducer<TState>
    {
        public TState Reduce(TState state, ReduxAction action)
        {
            return state;
        }
    }
}
