using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Primitives
{
    /// <summary>
    /// Represents a primitive Redux store.
    /// </summary>
    public interface IStore<TState>
    {
        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        void Dispatch(ReduxAction action);

        /// <summary>
        /// Returns the state of the store.
        /// </summary>
        TState State
        {
            get;
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        event StateChangedEventHandler<TState> StateChanged;

        /// <summary>
        /// Replaces the existing reducer with a new reducer.
        /// </summary>
        /// <param name="reducer">Reducer to be replaced with</param>
        void ReplaceReducer(Reducer<TState> nextReducer);
    }
}
