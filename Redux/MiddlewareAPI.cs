using System;

namespace Redux
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    public struct MiddlewareApi<TState>
    {
        internal Action<ReduxAction> Dispatch;
        internal Func<TState> GetState;
    }
}
