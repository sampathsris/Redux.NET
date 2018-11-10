
namespace Redux
{
    /// <summary>
    /// Represents an action that is used to defer action dispatching by standard
    /// thunk middleware (Such as the middleware returned by 
    /// <code>Redux.StandardMiddleware.CreateThunkMiddleware</code>).
    /// </summary>
    
    // Why is the Payload of this class of type `string`? Why not do the same as
    // redux-thunk JS module and make the payload take the type
    // `ReduxThunk<TState>`? This is because a delegate is hard to
    // serialize (yes, there are hacks which are too complicated). When we use a
    // `ReduxAction<string>`, we can serialize it just fine. The middleware will
    // retrieve the real delegate/lambda from the `ThunkActionRegistry` and
    // invoke it.
    // We also get to keep this class non-generic (i.e. no <TState>).
    public class ThunkAction: ReduxAction<string>
    {
        internal static string THUNK_ACTION_NAME = "__THUNK__";

        /// <summary>
        /// Creates a ThunkAction for a given thunk name.
        /// </summary>
        /// <param name="thunkName">Name of the thunk.</param>
        public ThunkAction(string thunkName)
            : base(THUNK_ACTION_NAME, thunkName) { }
    }
}
