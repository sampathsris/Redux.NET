using System;

namespace Redux
{
    /// <summary>
    /// Represents an action that is used to defer action dispatching by standard
    /// thunk middleware (Such as the middleware returned by 
    /// <code>Redux.StandardMiddleware.CreateThunkMiddleware</code>).
    /// </summary>
    /// <typeparam name="TState">Type of state of the target store.</typeparam>
    /// <typeparam name="TExtra">Type of the extra argument of the thunk.</typeparam>
    
    // Why is the Payload of this class of type `string`? Why not do the same as
    // redux-thunk JS module and make the payload take the type
    // `ReduxThunk<TState>`? This is because a delegate is hard to
    // serialize (yes, there are hacks which are too complicated). When we use a
    // `ReduxAction<string>`, we can serialize it just fine. The middleware will
    // retrieve the real delegate/lambda from the `ThunkActionRegistry` and
    // invoke it.
    // We also get to keep this class non-generic (i.e. no <TState>).
    [Serializable]
    public class ThunkAction<TState, TExtra>: ReduxAction<string>
    {
        internal static string THUNK_ACTION_NAME = "__THUNK__";

        public ReduxThunk<TState, TExtra> Thunk { get; private set; }

        /// <summary>
        /// Creates a ThunkAction for a given thunk name.
        /// </summary>
        /// <param name="thunkName">Name of the thunk.</param>
        public ThunkAction(string thunkName, ReduxThunk<TState, TExtra> thunk)
            : base(THUNK_ACTION_NAME, thunkName)
        {
            Thunk = thunk;
        }
    }

    /// <summary>
    /// Represents an action that is used to defer action dispatching by standard
    /// thunk middleware (Such as the middleware returned by 
    /// <code>Redux.StandardMiddleware.CreateThunkMiddleware</code>).
    /// </summary>
    /// <typeparam name="TState">Type of state of the target store.</typeparam>
    
    // This is simply a wrapper for ThunkAction<TState, TExtra>.
    public class ThunkAction<TState> : ThunkAction<TState, object>
    {
        public ThunkAction(string thunkName, ReduxThunk<TState> thunk)
            : base(thunkName, (dispatch, getState, obj) => thunk(dispatch, getState)) { }
    }
}
