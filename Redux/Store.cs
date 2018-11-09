using System;
using System.Collections.Generic;

namespace Redux
{
    /// <summary>
    /// Represents a Redux store that manages the state of type T.
    /// </summary>
    /// <typeparam name="TState">Type of the state.</typeparam>
    internal class Store<TState> : IStore
    {
        private IReducer<TState> reducer;

        /// <summary>
        /// Gets the current state of the store.
        /// </summary>
        public TState State { get; private set; }

        public Store(IReducer<TState> reducer, Func<TState> getPreloadedState)
        {
            this.reducer = reducer;

            // Preload state.
            if (getPreloadedState != null)
            {
                State = getPreloadedState();
            }
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
                throw new ArgumentNullException("action", "Action must not be null when calling Dispatch.");
            }

            TState oldState = State;
            State = reducer.Reduce(State, action);

            // Emit a StateChange event only if the state has changed.
            if (!EqualityComparer<TState>.Default.Equals(oldState, State))
            {
                OnStateChange(State);
            }
        }

        protected void OnStateChange(TState state)
        {
            if (StateChanged != null)
            {
                StateChanged(this, state);
            }
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        public event StateChangedEventHandler<TState> StateChanged;
    }
}
