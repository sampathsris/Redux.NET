using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return (MiddlewareAPI<T> api) =>
                (Action<ReduxAction> next) =>
                    (ReduxAction action) =>
                    {
                        Console.WriteLine("========================================");
                        Console.WriteLine("Dispatching {0}", action);
                        next(action);
                        Console.WriteLine("New State: ");
                        Console.WriteLine(api.GetState());
                        Console.WriteLine("========================================");
                    };
        }
    }
}
