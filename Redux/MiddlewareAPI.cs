using System;

namespace Redux
{
    public struct MiddlewareAPI<T>
    {
        public Action<ReduxAction> Dispatch;
        public Func<T> GetState;
    }
}
