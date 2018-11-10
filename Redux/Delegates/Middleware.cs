using System;

namespace Redux
{
    /// <summary>
    /// Represents a Redux middleware.
    /// </summary>
    /// <typeparam name="TState">Type of the state of the store on which the
    /// middleware will operate.</typeparam>
    /// <param name="dispatch">A delegate that takes the form of a dispatcher.</param>
    /// <param name="getState">When invoked, returns the current state of the store.</param>
    /// <returns>A function which accepts the dispatch function of the next
    /// middleware, and returns another, possibly different dispatch function.
    /// </returns>
    public delegate MiddlewareImplementation<TState> Middleware<TState>(
        Action<ReduxAction> dispatch,
        Func<TState> getState
    );
}
