using System;

namespace Redux
{
    /// <summary>
    /// Represents a function that can create a Redux store.
    /// </summary>
    /// <typeparam name="TState">Type of the stored state.</typeparam>
    /// <param name="reducer">Reducer used on the state.</param>
    /// <param name="getPreloadedState">A function that returns the preloaded
    /// state for the store initiliazation.</param>
    /// <param name="enhancer">A store enhancer. See StoreEnhancer.</param>
    /// <returns>A redux store.</returns>
    public delegate IStore StoreCreator<TState>(
        Reducer<TState> reducer,
        // Why is this parameter `Func<TState> getPreloadedState` instead of
        // `TState preloadedState`? The answer is: it should be an optional
        // parameter, but if T is a value type, we cannot do that.
        Func<TState> getPreloadedState,
        StoreEnhancer<TState> enhancer
    );
}
