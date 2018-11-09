
namespace Redux
{
    internal class MiddlewareEnhancedStore<TState> : Store<TState>, IStore
    {
        public MiddlewareEnhancedStore(MiddlewareApi<TState> api)
            : base(new IdentityReducer<TState>(), null)
        {
            Dispatcher = api.Dispatch;
            GetState = api.GetState;
        }
    }
}
