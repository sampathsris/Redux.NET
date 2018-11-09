using System;

namespace Redux
{
    public static class StandardMiddleware
    {
        /// <summary>
        /// Creates a middleware that logs all dispatched actions and
        /// subsequent states to the standard output.
        /// </summary>
        /// <typeparam name="T">Type of the target store.</typeparam>
        /// <returns>A middleware that logs all dispatched actions and
        /// subsequent states to the standard output.</returns>
        public static Middleware<T> CreateStdoutLoggerMiddleware<T>()
        {
            return
                (MiddlewareApi<T> api) =>
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
    }
}
