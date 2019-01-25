using System;

namespace Redux
{
    /// <summary>
    /// Represents a function that can create a Redux store.
    /// </summary>
    /// <param name="reducer">Reducer used on the state.</param>
    /// <param name="getPreloadedState">A function that returns the preloaded
    /// state for the store initiliazation.</param>
    /// <param name="enhancer">A store enhancer. See StoreEnhancer.</param>
    /// <returns>A redux store.</returns>
    public delegate IStore StoreCreator(
        Reducer reducer,
        // Why is this parameter `Func<IState> getPreloadedState` instead of
        // `IState preloadedState`? The answer is: it should be an optional
        // parameter, but if T is a value type, we cannot do that.
        Func<IState> getPreloadedState,
        StoreEnhancer enhancer
    );
}
