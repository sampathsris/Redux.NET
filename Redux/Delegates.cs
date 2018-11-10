using System;

namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <typeparam name="TState">Type of the state.</typeparam>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    public delegate void StateChangedEventHandler<TState>(
        IStore store,
        TState state
    );

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
        IReducer<TState> reducer,
        // Why is this parameter `Func<TState> getPreloadedState` instead of
        // `TState preloadedState`? The answer is: it should be an optional
        // parameter, but if T is a value type, we cannot do that.
        Func<TState> getPreloadedState,
        StoreEnhancer<TState> enhancer
    );

    /// <summary>
    /// Represents a function that accepts a StoreCreator, enhances it,
    /// and returns a new store creator.
    /// </summary>
    /// <typeparam name="TState">Type of the stored state.</typeparam>
    /// <param name="storeCreator">StoreCreator to be enhanced.</param>
    /// <returns>Enhanced StoreCreator.</returns>
    public delegate StoreCreator<TState> StoreEnhancer<TState>(
        StoreCreator<TState> storeCreator
    );

    /// <summary>
    /// Represents an implementation of a middleware. This is a function that
    /// accepts the dispatch function of the next middleware in the middleware
    /// chain, and returns another, possibly different dispatch function.
    /// </summary>
    /// <typeparam name="TState">Type of the store on which the middleware is working</typeparam>
    /// <param name="next">Next middleware in the chain.</param>
    /// <returns>A function which may or may not call the next middleware with
    /// a potentially different argument, and/or even at a later time.</returns>
    public delegate Action<ReduxAction> MiddlewareImplementation<TState>(
        Action<ReduxAction> next
    );

    /// <summary>
    /// Represents a Redux middleware.
    /// </summary>
    /// <typeparam name="TState">Type of the state of the store on which the
    /// middleware will operate.</typeparam>
    /// <param name="api">MiddlewareAPI that is passed to the middleware.</param>
    /// <returns>A function which accepts the dispatch function of the next
    /// middleware, and returns another, possibly different dispatch function.
    /// </returns>
    public delegate MiddlewareImplementation<TState> Middleware<TState>(
        IReduxDispatcherApi<TState> api
    );

    /// <summary>
    /// Represents a Redux thunk.
    /// </summary>
    /// <typeparam name="TState">Type of the sate of the store that this thunk
    /// will operate on.</typeparam>
    /// <param name="api">A reference to the redux dispatcher.</param>
    public delegate void ReduxThunk<TState>(
        IReduxDispatcherApi<TState> api
    );

    /// <summary>
    /// Represents a Redux thunk with an extra argument.
    /// </summary>
    /// <typeparam name="TState">Type of the sate of the store that this thunk
    /// will operate on.</typeparam>
    /// <typeparam name="TExtra">Type of the extra argument.</typeparam>
    /// <param name="api">A reference to the redux dispatcher.</param>
    /// <param name="extraArgument">Extra argument that can be used from within
    /// the thunk.</param>
    public delegate void ReduxThunk<TState, TExtra>(
        IReduxDispatcherApi<TState> api,
        TExtra extraArgument
    );
}
