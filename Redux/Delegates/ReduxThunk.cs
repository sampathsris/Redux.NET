using System;

namespace Redux
{
    /// <summary>
    /// Represents a Redux thunk.
    /// </summary>
    /// <typeparam name="TState">Type of the sate of the store that this thunk
    /// will operate on.</typeparam>
    /// <param name="dispatch">A delegate that takes the form of a dispatcher.</param>
    /// <param name="getState">When invoked, returns the current state of the store.</param>
    public delegate void ReduxThunk<TState>(
        Action<ReduxAction> dispatch,
        Func<TState> getState
    );

    /// <summary>
    /// Represents a Redux thunk with an extra argument.
    /// </summary>
    /// <typeparam name="TState">Type of the sate of the store that this thunk
    /// will operate on.</typeparam>
    /// <typeparam name="TExtra">Type of the extra argument.</typeparam>
    /// <param name="dispatch">A delegate that takes the form of a dispatcher.</param>
    /// <param name="getState">When invoked, returns the current state of the store.</param>
    /// <param name="extraArgument">Extra argument that can be used from within
    /// the thunk.</param>
    public delegate void ReduxThunk<TState, TExtra>(
        Action<ReduxAction> dispatch,
        Func<TState> getState,
        TExtra extraArgument
    );
}
