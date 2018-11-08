using System;
using System.Collections.Generic;
using System.Linq;

namespace Redux
{
    public static class Ops
    {
        /// <summary>
        /// Creates a Redux store that serves a given type T.
        /// </summary>
        /// <typeparam name="T">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<T>(
            IReducer<T> reducer,
            Func<T> getPreloadedState = null,
            StoreEnhancer<T> enhancer = null)
        {
            // if we have an enhancer, we want to call an enhanced CreateStore
            // function that is returned by the enhancer.
            if (enhancer != null)
            {
                return enhancer(CreateStore)(reducer, getPreloadedState);
            }

            return new Store<T>(reducer, getPreloadedState);
        }

        /// <summary>
        /// Combines a list of reducers into a single reducer.
        /// </summary>
        /// <param name="reducers">An array or parameter list of reducers. All reducers should be of type
        /// <code>Redux.Reducer&lt;T&gt;</code>. Only reason for this parameter to have <code>dynamic</code>
        /// type is to facilitate reducers with both value and reference types.</param>
        /// <returns>A combined reducer.</returns>
        public static IReducer<CombinedState> CombineReducers(IDictionary<string, object> reducerMapping)
        {
            foreach (var reducerkvp in reducerMapping)
            {
                // Check if each of the reducers are valid reducers. We simply check if the
                // object implements IReducer<>.
                if (!reducerkvp.Value.GetType()
                    .GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IReducer<>)
                    ))
                {
                    throw new ArgumentException("Invalid Reducer.");
                }
            }

            return new CombinedReducer(reducerMapping);
        }

        /// <summary>
        /// Takes a list of middleware and converts them into an store enhancer.
        /// </summary>
        /// <typeparam name="T">Type of the state of the target store.</typeparam>
        /// <param name="middlewareList">A parameter list of middleware to compose.</param>
        /// <returns>An enhancer function returns an enhanced CreateStore method,
        /// which will bind middleware to the created store.</returns>
        public static StoreEnhancer<T> ApplyMiddleware<T>(params Middleware<T>[] middlewareList)
        {
            return (StoreCreator<T> storeCreator) =>
                (IReducer<T> reducer, Func<T> getPreloadedState, StoreEnhancer<T> enhancer) =>
                {
                    IStore store = storeCreator(reducer, getPreloadedState, enhancer);

                    // Create a dummy dispatcher. This will be later assigned with
                    // the dispatcher created by composing middleware.
                    Action<ReduxAction> dispatch = (ReduxAction action) => {
                        throw new Exception(
                            "Dispatching while constructing the middleware is not " +
                            "allowed. Other middleware would not be applied.");
                    };

                    MiddlewareAPI<T> api = new MiddlewareAPI<T> {
                        // Do not assign `dispatch` to `Disptach`, but implement a lambda
                        // that calls `dispatch`. This is because `dispatch` will change
                        // later.
                        Dispatch = (action) => dispatch(action),
                        GetState = () => store.GetState<T>()
                    };

                    // Map each middleware to a function that accepts the middleware and
                    // calls it with the MiddlewareAPI.
                    IEnumerable<MiddlewareImplementation<T>> chain = middlewareList
                        .Select<Middleware<T>, MiddlewareImplementation<T>>(
                            middleware => middleware(api));

                    // Compose the functions in the above list into a single middleware.
                    MiddlewareImplementation<T> composedMiddleware = MiddlewareCompose<T>(chain);

                    // Get the wrapped dispatcher by calling the composed middleware.
                    dispatch = composedMiddleware(store.Dispatch);

                    return new MiddlewareEnhancedStore<T>(api, store);
            };
        }

        internal static MiddlewareImplementation<T> MiddlewareCompose<T>(
            IEnumerable<MiddlewareImplementation<T>> chain)
        {
            // If the list of functions is a, b, c, ..., z then this will compose the functions
            // such that the returned function is (arg) => a(b(c(...(z(arg))...))).
            // Composition will happen right-to-left.
            return (dispatch) => chain
                .Reverse()
                .Aggregate<MiddlewareImplementation<T>, Action<ReduxAction>>(
                    dispatch,
                    (acc, func) => func(acc)
                );
        }

        internal static Store<T> GetStore<T>(IStore store)
        {
            Store<T> realStore = store as Store<T>;

            if (realStore == null)
            {
                throw new InvalidOperationException("Store does not contain the queried type.");
            }

            return realStore;
        }
    }
}
