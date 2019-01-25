using System;

namespace Redux
{
    /// <summary>
    /// Represents an implementation of a middleware. This is a function that
    /// accepts the dispatch function of the next middleware in the middleware
    /// chain, and returns another, possibly different dispatch function.
    /// </summary>
    /// <param name="next">Next middleware in the chain.</param>
    /// <returns>A function which may or may not call the next middleware with
    /// a potentially different argument, and/or even at a later time.</returns>
    public delegate Action<ReduxAction> MiddlewareImplementation(
        Action<ReduxAction> next
    );
}
