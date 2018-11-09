
namespace Redux
{
    internal class MiddlewareEnhancedStore<TState> : Store<TState>, IStore
    {
        private static MiddlewareApi<TState> NULL_API = new MiddlewareApi<TState>
        {
            Dispatch = (action) => { }
        };

        private MiddlewareApi<TState> api = NULL_API;

        public MiddlewareEnhancedStore(MiddlewareApi<TState> api, IStore store)
            : base(new IdentityReducer<TState>(), null)
        {
            this.api = api;
            store.Subscribe<TState>(OnInnerStoreStateChange);
        }

        private void OnInnerStoreStateChange(IStore store, TState state)
        {
            OnStateChange(state);
        }

        public new TState State {
            get { return api.GetState(); }
        }

        public override void Dispatch(ReduxAction action)
        {
            api.Dispatch(action);
        }
    }
}
