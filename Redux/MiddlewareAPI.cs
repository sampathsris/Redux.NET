using System;

namespace Redux
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    public struct MiddlewareApi<T>
    {
        internal Action<ReduxAction> Dispatch;
        internal Func<T> GetState;
    }
}
