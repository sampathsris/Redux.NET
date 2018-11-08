
using System;
namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <typeparam name="T">Type of the state.</typeparam>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    public delegate void StateChangedEventHandler<T>(
        IStore store,
        T state
    );

    /// <summary>
    /// Represents a function that can create a Redux store.
    /// </summary>
    /// <typeparam name="T">Type of the stored state.</typeparam>
    /// <param name="reducer">Reducer used on the state.</param>
    /// <param name="getPreloadedState">A function that returns the preloaded
    /// state for the store initiliazation.</param>
    /// <param name="enhancer">A store enhancer. See StoreEnhancer.</param>
    /// <returns>A redux store.</returns>
    public delegate IStore StoreCreator<T>(
        IReducer<T> reducer,
        // Why is this parameter `Func<T> getPreloadedState` instead of
        // `T preloadedState`? The answer is: it should be an optional parameter,
        // but if T is a value type, we cannot do that.
        Func<T> getPreloadedState = null,
        StoreEnhancer<T> enhancer = null
    );

    /// <summary>
    /// Represents a function that accepts a StoreCreator, enhances it,
    /// and returns a new store creator.
    /// </summary>
    /// <typeparam name="T">Type of the stored state.</typeparam>
    /// <param name="storeCreator">StoreCreator to be enhanced.</param>
    /// <returns>Enhanced StoreCreator.</returns>
    public delegate StoreCreator<T> StoreEnhancer<T>(
        StoreCreator<T> storeCreator
    );

    /// <summary>
    /// Represents an implementation of a middleware. This is a function that
    /// accepts the dispatch function of the next middleware in the middleware
    /// chain, and returns another, possibly different dispatch function.
    /// </summary>
    /// <typeparam name="T">Type of the store on which the middleware is working</typeparam>
    /// <param name="next">Next middleware in the chain.</param>
    /// <returns>A function which may or may not call the next middleware with
    /// a potentially different argument, and/or even at a later time.</returns>
    public delegate Action<ReduxAction> MiddlewareImplementation<T>(
        Action<ReduxAction> next
    );

    /// <summary>
    /// Represents a Redux middleware.
    /// </summary>
    /// <typeparam name="T">Type of the state of the store on which the
    /// middleware will operate.</typeparam>
    /// <param name="api">MiddlewareAPI that is passed to the middleware.</param>
    /// <returns>A function which accepts the dispatch function of the next
    /// middleware, and returns another, possibly different dispatch function.
    /// </returns>
    public delegate MiddlewareImplementation<T> Middleware<T>(
        MiddlewareAPI<T> api
    );
}
