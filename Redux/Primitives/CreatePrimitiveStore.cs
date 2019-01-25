
using System;
using System.Collections.Generic;
namespace Redux
{
    public static partial class Ops
    {

        /// <summary>
        /// Creates a redux store that reduces a primitive state of type TState.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        /// <param name="premitiveReducer">A primitive reducer function.</param>
        /// <param name="preloadedState">Primitive state to pre-load.</param>
        /// <param name="enhancer">Enhancer function that enhances the store.</param>
        /// <returns>The created Redux store.</returns>
        public static PrimitiveStore<TState> CreateStore<TState>(
            Func<TState, ReduxAction, TState> premitiveReducer,
            TState preloadedState = default(TState),
            StoreEnhancer enhancer = null) where TState : struct
        {
            Reducer reducer = (IState state, ReduxAction action) =>
            {
                var primitiveState = state as PrimitiveState<TState>;

                if (primitiveState == null)
                {
                    throw new ArgumentException("Wrong type used on primitive reducer.", "state");
                }

                TState prevState = primitiveState.Payload;
                TState nextState = premitiveReducer(prevState, action);

                if (EqualityComparer<TState>.Default.Equals(prevState, nextState))
                {
                    return state;
                }

                return new PrimitiveState<TState>(nextState);
            };

            Func<IState> getPreloadedState = () => new PrimitiveState<TState>(preloadedState);

            var wrappedStore = CreateStore(reducer, getPreloadedState, enhancer);
            return new PrimitiveStore<TState>(wrappedStore);
        }
    }

    internal class PrimitiveState<TState> : IState
    {
        public PrimitiveState(TState payload)
        {
            Payload = payload;
        }

        public TState Payload { get; set; }
    }

    /// <summary>
    /// Represents a Redux store that could host a primitive value.
    /// </summary>
    /// <typeparam name="TState">Type of the primitive value</typeparam>
    public class PrimitiveStore<TState> : IStore where TState: struct
    {
        private IStore WrapperStore { get; set; }

        public TState State
        {
            get
            {
                var state = WrapperStore.GetState() as PrimitiveState<TState>;
                return state.Payload;
            }
        }

        public PrimitiveStore(IStore wrapperStore)
        {
            WrapperStore = wrapperStore;

            WrapperStore.GetStore().StateChanged +=WrapperStore_StateChanged;
        }

        private void WrapperStore_StateChanged(IStore store, IState state)
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
        public event PrimitiveStateChangedEventHandler<TState> StateChanged;
    }
}
