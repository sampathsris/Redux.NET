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
        /// <summary>
        /// The reducer that is used by the store.
        /// </summary>
        public Reducer<TState> Reducer { get; private set; }

        /// <summary>
        /// The dispatcher that is invoked from the IStore.Dispatch implementation.
        /// </summary>
        public Action<ReduxAction> Dispatcher { get; set; }

        private TState state;

        /// <summary>
        /// When invoked, returns the current state of the store.
        /// </summary>
        public Func<TState> GetState { get; set; }

        public Store(Reducer<TState> reducer, Func<TState> getPreloadedState)
        {
            Reducer = reducer;

            // initialize dispatcher.
            Dispatcher = (action) =>
            {
                state = reducer(state, action);
            };

            // initialize the state getter.
            GetState = () => state;

            // Preload state.
            if (getPreloadedState != null)
            {
                state = getPreloadedState();
            }
        }

        /// <summary>
        /// Dispatches an action to the Store.
        /// </summary>
        /// <param name="action">Action to be dispatched.</param>
        public void Dispatch(ReduxAction action)
        {
            // Action must not be null.
            if (action == null)
            {
                throw new ArgumentNullException("action", Properties.Resources.DISPATCH_ACTION_NULL_ERROR);
            }

            TState currentState = GetState();
            InvokeDispatcher(action);
            TState nextState = GetState();

            // Emit a StateChange event only if the state has changed.
            if (!EqualityComparer<TState>.Default.Equals(currentState, nextState))
            {
                OnStateChange(nextState);
            }
        }

        private void InvokeDispatcher(ReduxAction action)
        {
            Dispatcher(action);
        }

        private void OnStateChange(TState nextState)
        {
            if (StateChanged != null)
            {
                StateChanged(this, nextState);
            }
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        public event StateChangedEventHandler<TState> StateChanged;
    }
}
