using System;

namespace Redux
{
    /// <summary>
    /// Represents a Redux middleware.
    /// </summary>
    /// <param name="dispatch">A delegate that takes the form of a dispatcher.</param>
    /// <param name="getState">When invoked, returns the current state of the store.</param>
    /// <returns>A function which accepts the dispatch function of the next
    /// middleware, and returns another, possibly different dispatch function.
    /// </returns>
    public delegate MiddlewareImplementation Middleware(
        Action<ReduxAction> dispatch,
        Func<IState> getState
    );
}
