using System;

namespace Redux
{
    internal class MiddlewareEnhancedStore<TState> : Store<TState>, IStore
    {
        public MiddlewareEnhancedStore(IReduxDispatcherApi<TState> concreteDispatcher)
            : base(new IdentityReducer<TState>(), null)
        {
            Dispatcher = concreteDispatcher.Dispatcher;
            GetState = concreteDispatcher.GetState;
        }
    }
}
