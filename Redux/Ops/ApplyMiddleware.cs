using System;
using System.Linq;
using System.Collections.Generic;

namespace Redux
{
    public static partial class Ops
    {
        /// <summary>
        /// Takes a list of middleware and converts them into an store enhancer.
        /// </summary>
        /// <typeparam name="TState">Type of the state of the target store.</typeparam>
        /// <param name="middlewareList">A parameter list of middleware to compose.</param>
        /// <returns>An enhancer function returns an enhanced CreateStore method,
        /// which will bind middleware to the created store.</returns>
        public static StoreEnhancer<TState> ApplyMiddleware<TState>(params Middleware<TState>[] middlewareList)
        {
            return (StoreCreator<TState> storeCreator) =>
                (Reducer<TState> reducer, Func<TState> getPreloadedState, StoreEnhancer<TState> enhancer) =>
                {
                    IStore store = storeCreator(reducer, getPreloadedState, enhancer);
                    var realStore = store.GetStore<TState>();
                    Action<ReduxAction> originalDispatch = realStore.Dispatcher;
                    Func<TState> getState = realStore.GetState;

                    // Create a dummy dispatcher. This will be later assigned with
                    // the dispatcher created by composing middleware.
                    Action<ReduxAction> dispatch = (ReduxAction action) =>
                    {
                        throw new System.InvalidOperationException(
                            "Dispatching while constructing the middleware is not " +
                            "allowed. Other middleware would not be applied.");
                    };

                    // Map each middleware to a function that accepts the middleware and
                    // calls it with the MiddlewareAPI.
                    IEnumerable<MiddlewareImplementation<TState>> chain = middlewareList
                        .Select<Middleware<TState>, MiddlewareImplementation<TState>>(
                            middleware => middleware((action) => dispatch(action), getState));

                    // Compose the functions in the above list into a single middleware.
                    MiddlewareImplementation<TState> composedMiddleware = ComposeMiddleware<TState>(chain);

                    // Get the wrapped dispatcher by calling the composed middleware.
                    dispatch = composedMiddleware(originalDispatch);
                    realStore.Dispatcher = dispatch;

                    return store;
                };
        }

        private static MiddlewareImplementation<TState> ComposeMiddleware<TState>(
            IEnumerable<MiddlewareImplementation<TState>> middlewareChain)
        {
            // If the list of functions is a, b, c, ..., z then this will compose the functions
            // such that the returned function is (arg) => a(b(c(...(z(arg))...))).
            // Composition will happen right-to-left.
            return (dispatch) => middlewareChain
                .Reverse()
                .Aggregate<MiddlewareImplementation<TState>, Action<ReduxAction>>(
                    dispatch,
                    (acc, func) => func(acc)
                );
        }
    }
}
