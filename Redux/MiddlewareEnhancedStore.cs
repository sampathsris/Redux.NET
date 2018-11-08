
namespace Redux
{
    internal class MiddlewareEnhancedStore<T> : Store<T>, IStore
    {
        private static MiddlewareAPI<T> NULL_API = new MiddlewareAPI<T>
        {
            Dispatch = (action) => { }
        };

        private MiddlewareAPI<T> api = NULL_API;
        private IStore store;

        public MiddlewareEnhancedStore(MiddlewareAPI<T> api, IStore store)
            : base(new IdentityReducer<T>(), null)
        {
            this.api = api;
            this.store = store;
            store.Subscribe<T>(OnInnerStoreStateChange);
        }

        private void OnInnerStoreStateChange(IStore store, T state)
        {
            OnStateChange(state);
        }

        public new T State {
            get { return api.GetState(); }
        }

        public override void Dispatch(ReduxAction action)
        {
            api.Dispatch(action);
        }
    }
}
