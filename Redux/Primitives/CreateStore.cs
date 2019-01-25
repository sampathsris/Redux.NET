
using System;

namespace Redux.Primitives
{
    public static partial class Ops<TState> where TState : struct
    {
        /// <summary>
        /// Creates a redux store that reduces a primitive state of type TState.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="primitiveReducer">A primitive reducer function.</param>
        /// <param name="preloadedState">Primitive state to pre-load.</param>
        /// <param name="enhancer">Enhancer function that enhances the store.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore<TState> CreateStore(
            Reducer<TState> primitiveReducer,
            TState preloadedState = default(TState),
            StoreEnhancer enhancer = null)
        {
            Reducer reducer = WrapReducer(primitiveReducer);
            Func<IState> getPreloadedState = () => new StateWrapper<TState>(preloadedState);

            var wrappedStore = Redux.Ops.CreateStore(reducer, getPreloadedState, enhancer);
            return new Store<TState>(wrappedStore);
        }
    }
}
