
namespace Redux
{
    internal class MiddlewareEnhancedStore<T> : Store<T>
    {
        private MiddlewareAPI<T> api;

        public MiddlewareEnhancedStore(MiddlewareAPI<T> api)
            : base(new IdentityReducer<T>())
        {
            this.api = api;
        }

        public new T State {
            get { return api.GetState(); }
        }

        public new void Dispatch(ReduxAction action)
        {
            api.Dispatch(action);
        }
    }
}
