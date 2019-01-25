using System;

namespace Redux
{
    public static partial class Ops
    {
        /// <summary>
        /// Creates a Redux store.
        /// </summary>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore(
            Reducer reducer)
        {
            return CreateStore(reducer, null, null);
        }

        /// <summary>
        /// Creates a Redux store with pre-loaded state.
        /// </summary>
        /// <param name="reducer">Reducer function.</param>
        /// <param name="getPreloadedState">A function that can be called to retrieve
        /// the initial state to pre-load the store.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore(
            Reducer reducer,
            Func<IState> getPreloadedState)
        {
            return CreateStore(reducer, getPreloadedState, null);
        }

        /// <summary>
        /// Creates a Redux store with pre-loaded state and an enhancer.
        /// </summary>
        /// <param name="reducer">Reducer function.</param>
        /// <param name="getPreloadedState">A function that can be called to retrieve
        /// the initial state to pre-load the store.</param>
        /// <param name="enhancer">Enhancer function that enhances the store.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore(
            Reducer reducer,
            Func<IState> getPreloadedState,
            StoreEnhancer enhancer)
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
                store = new Store(reducer, getPreloadedState);
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
