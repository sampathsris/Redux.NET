using System;

namespace Redux
{
    public static class StandardMiddleware
    {
        /// <summary>
        /// Creates a middleware that logs all dispatched actions and
        /// subsequent states to the standard output.
        /// </summary>
        /// <typeparam name="TState">Type of the target store.</typeparam>
        /// <returns>A middleware that logs all dispatched actions and
        /// subsequent states to the standard output.</returns>
        public static Middleware<TState> CreateStdoutLoggerMiddleware<TState>()
        {
            return
                (IReduxDispatcherApi<TState> api) =>
                    (Action<ReduxAction> next) =>
                        (ReduxAction action) =>
                {
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_ACTION_SEPARATOR);
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_DISPATCHING, action);
                    next(action);
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_NEW_STATE);
                    Console.WriteLine(api.GetState());
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_ACTION_SEPARATOR);
                };
        }

        /// <summary>
        /// Creates a middleware that can intercept <code>ThunkAction</code>s, and dispatch
        /// a different action, possibly at a later time, or even not at all. Also accepts
        /// an extra argument, which will be passed to the thunk when it executes.
        /// </summary>
        /// <typeparam name="TState">Type of the state of the store.</typeparam>
        /// <typeparam name="TExtra">Type of the extra argument.</typeparam>
        /// <param name="extraArgument">An extra argument to be passed to the payload function.</param>
        /// <returns>The created thunk middleware.</returns>
        public static Middleware<TState> CreateThunkMiddleware<TState, TExtra>(TExtra extraArgument)
        {
            return
                (IReduxDispatcherApi<TState> api) =>
                    (Action<ReduxAction> next) =>
                        (ReduxAction action) =>
                {
                    var thunkAction = action as ThunkAction<TState, TExtra>;

                    if (thunkAction != null)
                    {
                        thunkAction.Thunk(api, extraArgument);
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
        public static Middleware<TState> CreateThunkMiddleware<TState>()
        {
            return CreateThunkMiddleware<TState, object>(null);
        }
    }
}
