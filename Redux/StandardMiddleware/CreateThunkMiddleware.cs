using System;

namespace Redux
{
    public static partial class StandardMiddleware
    {

        /// <summary>
        /// Creates a middleware that can intercept <code>ThunkAction</code>s, and dispatch
        /// a different action, possibly at a later time, or even not at all. Also accepts
        /// an extra argument, which will be passed to the thunk when it executes.
        /// </summary>
        /// <typeparam name="TExtra">Type of the extra argument.</typeparam>
        /// <param name="extraArgument">An extra argument to be passed to the payload function.</param>
        /// <returns>The created thunk middleware.</returns>
        public static Middleware CreateThunkMiddleware<TExtra>(TExtra extraArgument)
        {
            return
                (Action<ReduxAction> dispatch, Func<IState> getState) =>
                    (Action<ReduxAction> next) =>
                        (ReduxAction action) =>
                {
                    var thunkAction = action as ThunkAction<TExtra>;

                    if (thunkAction != null)
                    {
                        thunkAction.Thunk(dispatch, getState, extraArgument);
                    }

                    next(action);
                };
        }

        /// <summary>
        /// Creates a middleware that can intercept <code>ThunkAction</code>s, and dispatch
        /// a different action, possibly at a later time, or even not at all.
        /// </summary>
        /// <typeparam name="TState">Type of the state of the store.</typeparam>
        /// <param name="extraArgument">An extra argument to be passed to the payload function.</param>
        /// <returns>The created thunk middleware.</returns>
        public static Middleware CreateThunkMiddleware()
        {
            return CreateThunkMiddleware<object>(null);
        }
    }
}
