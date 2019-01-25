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
        /// <param name="middlewareList">A parameter list of middleware to compose.</param>
        /// <returns>An enhancer function returns an enhanced CreateStore method,
        /// which will bind middleware to the created store.</returns>
        public static StoreEnhancer ApplyMiddleware(params Middleware[] middlewareList)
        {
            return (StoreCreator storeCreator) =>
                (Reducer reducer, Func<IState> getPreloadedState, StoreEnhancer enhancer) =>
                {
                    IStore store = storeCreator(reducer, getPreloadedState, enhancer);
                    var realStore = store as Store;
                    // No need to null-check realStore, as we are guaranteed that store
                    // must be an instance of Store.

                    Action<ReduxAction> originalDispatch = realStore.Dispatcher;
                    Func<IState> getState = realStore.GetState;

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
                    IEnumerable<MiddlewareImplementation> chain = middlewareList
                        .Select<Middleware, MiddlewareImplementation>(
                            middleware => middleware((action) => dispatch(action), getState));

                    // Compose the functions in the above list into a single middleware.
                    MiddlewareImplementation composedMiddleware = ComposeMiddleware(chain);

                    // Get the wrapped dispatcher by calling the composed middleware.
                    dispatch = composedMiddleware(originalDispatch);
                    realStore.Dispatcher = dispatch;

                    return store;
                };
        }

        private static MiddlewareImplementation ComposeMiddleware(
            IEnumerable<MiddlewareImplementation> middlewareChain)
        {
            // If the list of functions is a, b, c, ..., z then this will compose the functions
            // such that the returned function is (arg) => a(b(c(...(z(arg))...))).
            // Composition will happen right-to-left.
            return (dispatch) => middlewareChain
                .Reverse()
                .Aggregate<MiddlewareImplementation, Action<ReduxAction>>(
                    dispatch,
                    (acc, func) => func(acc)
                );
        }
    }
}
