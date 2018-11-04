﻿using System;
using System.Collections.Generic;

namespace Redux
{
    /// <summary>
    /// Represents a Redux store that holds a reference type.
    /// </summary>
    /// <typeparam name="T">Type of the state.</typeparam>
    internal class ReferenceTypeStore<T>: IStore
    {
        private Reducer<T> reducer;

        /// <summary>
        /// Gets the current state of the store.
        /// </summary>
        public T State { get; private set; }

        public ReferenceTypeStore(Reducer<T> reducer)
        {
            this.reducer = reducer;
            // Initialize the store.
            Dispatch(Action.__INIT__);
        }

        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        public void Dispatch(Action action)
        {
            // Action must not be null.
            if (action == null)
            {
                throw new ArgumentNullException("Action", "Action must not be null when calling Dispatch.");
            }

            T oldState = State;
            State = reducer(State, action);

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