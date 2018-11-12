using System;

namespace Redux
{
    public static partial class StandardMiddleware
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
                (Action<ReduxAction> dispatch, Func<TState> getState) =>
                    (Action<ReduxAction> next) =>
                        (ReduxAction action) =>
                {
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_ACTION_SEPARATOR);
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_DISPATCHING, action);
                    next(action);
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_NEW_STATE);
                    Console.WriteLine(getState());
                    Console.WriteLine(Properties.Resources.STDOUT_LOGGER_MW_ACTION_SEPARATOR);
                };
        }
    }
}
