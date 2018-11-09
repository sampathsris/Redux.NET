﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Redux
{
    public static class Ops
    {
        /// <summary>
        /// Creates a Redux store that serves a given type TState.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<TState>(
            IReducer<TState> reducer)
        {
            return CreateStore<TState>(reducer, null, null);
        }

        /// <summary>
        /// Creates a Redux store that serves a given type TState with pre-loaded state.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <param name="getPreloadedState">A function that can be called to retrieve
        /// the initial state to pre-load the store.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<TState>(
            IReducer<TState> reducer,
            Func<TState> getPreloadedState)
        {
            return CreateStore<TState>(reducer, getPreloadedState, null);
        }

        /// <summary>
        /// Creates a Redux store that serves a given type TState with pre-loaded state and
        /// an enhancer.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <param name="getPreloadedState">A function that can be called to retrieve
        /// the initial state to pre-load the store.</param>
        /// <param name="enhancer">Enhancer function that enhances the store.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<TState>(
            IReducer<TState> reducer,
            Func<TState> getPreloadedState,
            StoreEnhancer<TState> enhancer)
        {
            IStore store;

            // if we have an enhancer, we want to call an enhanced CreateStore
            // function that is returned by the enhancer.
            if (enhancer != null)
            {
                store = enhancer(CreateStore)(reducer, getPreloadedState, null);
            }
            else
            {
                store = new Store<TState>(reducer, getPreloadedState);
            }

            // Initialize the store.
            // Why not call this within the Store<T>'s constructor? Because it would violate:
            // https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2214-do-not-call-overridable-methods-in-constructors?view=vs-2017
            // It would also mean that any middleware would not receive INIT action, which could
            // potentially be dangerous.
            store.Dispatch(ReduxAction.__INIT__);

            return store;
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
            if (reducerMapping == null) throw new ArgumentNullException("reducerMapping");

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
        /// <typeparam name="TState">Type of the state of the target store.</typeparam>
        /// <param name="middlewareList">A parameter list of middleware to compose.</param>
        /// <returns>An enhancer function returns an enhanced CreateStore method,
        /// which will bind middleware to the created store.</returns>
        public static StoreEnhancer<TState> ApplyMiddleware<TState>(params Middleware<TState>[] middlewareList)
        {
            return (StoreCreator<TState> storeCreator) =>
                (IReducer<TState> reducer, Func<TState> getPreloadedState, StoreEnhancer<TState> enhancer) =>
                {
                    IStore store = storeCreator(reducer, getPreloadedState, enhancer);

                    // Create a dummy dispatcher. This will be later assigned with
                    // the dispatcher created by composing middleware.
                    Action<ReduxAction> dispatch = (ReduxAction action) => {
                        throw new System.InvalidOperationException(
                            "Dispatching while constructing the middleware is not " +
                            "allowed. Other middleware would not be applied.");
                    };

                    MiddlewareApi<TState> api = new MiddlewareApi<TState> {
                        // Do not assign `dispatch` to `Disptach`, but implement a lambda
                        // that calls `dispatch`. This is because `dispatch` will change
                        // later.
                        Dispatch = (action) => dispatch(action),
                        GetState = () => store.GetState<TState>()
                    };

                    // Map each middleware to a function that accepts the middleware and
                    // calls it with the MiddlewareAPI.
                    IEnumerable<MiddlewareImplementation<TState>> chain = middlewareList
                        .Select<Middleware<TState>, MiddlewareImplementation<TState>>(
                            middleware => middleware(api));

                    // Compose the functions in the above list into a single middleware.
                    MiddlewareImplementation<TState> composedMiddleware = MiddlewareCompose<TState>(chain);

                    // Get the wrapped dispatcher by calling the composed middleware.
                    dispatch = composedMiddleware(store.Dispatch);

                    return new MiddlewareEnhancedStore<TState>(api);
            };
        }

        internal static MiddlewareImplementation<TState> MiddlewareCompose<TState>(
            IEnumerable<MiddlewareImplementation<TState>> chain)
        {
            // If the list of functions is a, b, c, ..., z then this will compose the functions
            // such that the returned function is (arg) => a(b(c(...(z(arg))...))).
            // Composition will happen right-to-left.
            return (dispatch) => chain
                .Reverse()
                .Aggregate<MiddlewareImplementation<TState>, Action<ReduxAction>>(
                    dispatch,
                    (acc, func) => func(acc)
                );
        }

        internal static Store<TState> GetStore<TState>(IStore store)
        {
            Store<TState> realStore = store as Store<TState>;

            if (realStore == null)
            {
                throw new InvalidOperationException("Store does not contain the queried type.");
            }

            return realStore;
        }
    }
}
