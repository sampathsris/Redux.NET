
using System;
using System.Collections.Generic;
namespace Redux.Primitives
{
    public static partial class Ops<TState> where TState : struct
    {
        /// <summary>
        /// Wraps a primitive reducer as a Redux.Reducer.
        /// </summary>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="reducer">Reducer to wrap</param>
        /// <returns></returns>
        public static Reducer WrapReducer(Reducer<TState> reducer)
        {
            return (IState state, ReduxAction action) =>
            {
                var primitiveState = state as StateWrapper<TState>;

                if (primitiveState == null)
                {
                    throw new ArgumentException("Wrong type used on primitive reducer.", "state");
                }

                TState prevState = primitiveState.Payload;
                TState nextState = reducer(prevState, action);

                if (EqualityComparer<TState>.Default.Equals(prevState, nextState))
                {
                    return state;
                }

                return new StateWrapper<TState>(nextState);
            };
        }
    }
}
