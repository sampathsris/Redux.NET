using System;

namespace Redux
{
    public static partial class Ops
    {
        /// <summary>
        /// Creates a Redux store that serves a given type TState.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<TState>(
            Reducer<TState> reducer)
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
            Reducer<TState> reducer,
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
            Reducer<TState> reducer,
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
            store.Dispatch(ReduxAction.InitAction);

            return store;
        }
    }
}
