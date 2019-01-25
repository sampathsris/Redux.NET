using System;
using System.Collections.Generic;

namespace Redux
{
    /// <summary>
    /// Represents a Redux store.
    /// </summary>
    internal class Store : IStore
    {
        /// <summary>
        /// The reducer that is used by the store.
        /// </summary>
        public Reducer Reducer { get; private set; }

        /// <summary>
        /// The dispatcher that is invoked from the IStore.Dispatch implementation.
        /// </summary>
        public Action<ReduxAction> Dispatcher { get; set; }

        private IState state;

        /// <summary>
        /// When invoked, returns the current state of the store.
        /// </summary>
        public Func<IState> GetState { get; set; }

        /// <summary>
        /// Returns the state of the store.
        /// </summary>
        public IState State
        {
            get
            {
                return GetState();
            }
        }

        public Store(Reducer reducer, Func<IState> getPreloadedState)
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

            IState currentState = GetState();
            InvokeDispatcher(action);
            IState nextState = GetState();

            // Emit a StateChange event only if the state has changed.
            if (!EqualityComparer<IState>.Default.Equals(currentState, nextState))
            {
                OnStateChange(nextState);
            }
        }

        public void ReplaceReducer(Reducer nextReducer)
        {
            Reducer = nextReducer;
            InvokeDispatcher(ReduxAction.ReplaceReducerAction);
        }

        private void InvokeDispatcher(ReduxAction action)
        {
            Dispatcher(action);
        }

        private void OnStateChange(IState nextState)
        {
            if (StateChanged != null)
            {
                StateChanged(this, nextState);
            }
        }

        /// <summary>
        /// Invoked whenever the store changes state.
        /// </summary>
        public event StateChangedEventHandler StateChanged;
    }
}
