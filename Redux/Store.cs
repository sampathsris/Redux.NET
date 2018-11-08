﻿using System;
using System.Collections.Generic;

namespace Redux
{
    /// <summary>
    /// Represents a Redux store that manages the state of type T.
    /// </summary>
    /// <typeparam name="T">Type of the state.</typeparam>
    internal class Store<T> : IStore
    {
        private IReducer<T> reducer;

        /// <summary>
        /// Gets the current state of the store.
        /// </summary>
        public T State { get; private set; }

        public Store(IReducer<T> reducer, Func<T> getPreloadedState)
        {
            this.reducer = reducer;

            // Preload state.
            if (getPreloadedState != null)
            {
                State = getPreloadedState();
            }

            // Initialize the store.
            Dispatch(ReduxAction.__INIT__);
        }

        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        public virtual void Dispatch(ReduxAction action)
        {
            // Action must not be null.
            if (action == null)
            {
                throw new ArgumentNullException("Action", "Action must not be null when calling Dispatch.");
            }

            T oldState = State;
            State = reducer.Reduce(State, action);

            // Emit a StateChange event only if the state has changed.
            if (!EqualityComparer<T>.Default.Equals(oldState, State))
            {
                OnStateChange();
            }
        }

        private void OnStateChange()
        {
            if (StateChanged != null)
            {
                StateChanged(this, State);
            }
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        public event StateChangedEventHandler<T> StateChanged;
    }
}
