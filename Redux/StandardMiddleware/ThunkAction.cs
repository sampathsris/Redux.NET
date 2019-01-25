using System;

namespace Redux
{
    /// <summary>
    /// Represents an action that is used to defer action dispatching by standard
    /// thunk middleware (Such as the middleware returned by 
    /// <code>Redux.StandardMiddleware.CreateThunkMiddleware</code>).
    /// </summary>
    /// <typeparam name="TExtra">Type of the extra argument of the thunk.</typeparam>
    public class ThunkAction<TExtra>: ReduxAction<string>
    {
        internal static string THUNK_ACTION_NAME = "__THUNK__";

        public ReduxThunk<TExtra> Thunk { get; private set; }

        /// <summary>
        /// Creates a ThunkAction for a given thunk name.
        /// </summary>
        /// <param name="thunkName">Name of the thunk.</param>
        public ThunkAction(string thunkName, ReduxThunk<TExtra> thunk)
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
    public class ThunkAction : ThunkAction<object>
    {
        public ThunkAction(string thunkName, ReduxThunk thunk)
            : base(thunkName, (dispatch, getState, obj) => thunk(dispatch, getState)) { }
    }
}
