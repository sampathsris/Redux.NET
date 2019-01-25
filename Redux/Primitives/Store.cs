using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Primitives
{
    /// <summary>
    /// Represents a Redux store that could host a primitive value.
    /// </summary>
    /// <typeparam name="TState">Type of the primitive value</typeparam>
    internal class Store<TState> : IStore<TState> where TState : struct
    {
        private IStore WrapperStore { get; set; }

        public TState State
        {
            get
            {
                var state = WrapperStore.State as StateWrapper<TState>;
                return state.Payload;
            }
        }

        public Store(IStore wrapperStore)
        {
            WrapperStore = wrapperStore;

            (WrapperStore as Redux.Store).StateChanged += WrapperStore_StateChanged;
        }

        private void WrapperStore_StateChanged(object sender, IState state)
        {
            if (StateChanged != null)
            {
                StateChanged(this, State);
            }
        }

        public void Dispatch(ReduxAction action)
        {
            WrapperStore.Dispatch(action);
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        public event StateChangedEventHandler<TState> StateChanged;

        public void ReplaceReducer(Reducer<TState> nextReducer)
        {
            WrapperStore.ReplaceReducer(Ops<TState>.WrapReducer(nextReducer));
        }
    }
}
