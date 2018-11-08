
namespace Redux
{
    internal class MiddlewareEnhancedStore<T> : Store<T>, IStore
    {
        private static MiddlewareApi<T> NULL_API = new MiddlewareApi<T>
        {
            Dispatch = (action) => { }
        };

        private MiddlewareApi<T> api = NULL_API;

        public MiddlewareEnhancedStore(MiddlewareApi<T> api, IStore store)
            : base(new IdentityReducer<T>(), null)
        {
            this.api = api;
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
