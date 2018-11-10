using System;

namespace Redux
{
    internal class MiddlewareEnhancedStore<TState> : Store<TState>, IStore
    {
        public MiddlewareEnhancedStore(Action<ReduxAction> dispatch, Func<TState> getState)
            : base(new IdentityReducer<TState>(), null)
        {
            Dispatcher = dispatch;
            GetState = getState;
        }
    }
}
