﻿
namespace Redux
{
    /// <summary>
    /// Represents a Redux store.
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        void Dispatch(ReduxAction action);

        /// <summary>
        /// Returns the state of the store.
        /// </summary>
        IState State
        {
            get;
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        event StateChangedEventHandler StateChanged;

        /// <summary>
        /// Replaces the existing reducer with a new reducer.
        /// </summary>
        /// <param name="reducer">Reducer to be replaced with</param>
        void ReplaceReducer(Reducer nextReducer);
    }
}
