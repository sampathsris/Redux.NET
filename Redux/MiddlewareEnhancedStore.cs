
namespace Redux
{
    internal class MiddlewareEnhancedStore<T> : Store<T>
    {
        private static MiddlewareAPI<T> NULL_API = new MiddlewareAPI<T>
        {
            Dispatch = (action) => { }
        };

        private MiddlewareAPI<T> api = NULL_API;

        public MiddlewareEnhancedStore(MiddlewareAPI<T> api)
            : base(new IdentityReducer<T>(), null)
        {
            this.api = api;
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
