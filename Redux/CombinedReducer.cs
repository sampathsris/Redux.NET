using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux
{
    internal class CombinedReducer: IReducer<CombinedState>
    {
        private IDictionary<string, dynamic> reducers;

        public CombinedReducer(IDictionary<string, object> reducers)
        {
            this.reducers = reducers;
        }

        public CombinedState Reduce(CombinedState state, Action action)
        {
            var nextState = new CombinedState(reducers);

            if (state == null)
            {
                state = nextState;
            }

            bool stateChanged = false;

            foreach (var reducerkvp in reducers)
            {
                dynamic componentState = state[reducerkvp.Key];
                dynamic nextComponentState = reducerkvp.Value.Reduce(componentState, action);

                if (componentState != nextComponentState)
                {
                    stateChanged = true;
                }

                nextState[reducerkvp.Key] = nextComponentState;
            }

            return (stateChanged ? nextState : state);
        }
    }
}
