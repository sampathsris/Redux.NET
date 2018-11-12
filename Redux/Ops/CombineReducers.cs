using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Redux
{
    public static partial class Ops
    {
        /// <summary>
        /// Combines a list of reducers into a single reducer.
        /// </summary>
        /// <param name="reducers">An array or parameter list of reducers. All reducers should be of type
        /// <code>Redux.Reducer&lt;T&gt;</code>. Only reason for this parameter to have <code>dynamic</code>
        /// type is to facilitate reducers with both value and reference types.</param>
        /// <returns>A combined reducer.</returns>
        public static Reducer<CombinedState> CombineReducers(IDictionary<string, object> reducerMapping)
        {
            if (reducerMapping == null) throw new ArgumentNullException("reducerMapping");

            IDictionary<string, MethodInfo> invokerMapping = new Dictionary<string, MethodInfo>();

            foreach (var reducerkvp in reducerMapping)
            {
                Type type = reducerkvp.Value.GetType();

                if (!(type.IsGenericType ||
                      type.GetGenericTypeDefinition() == typeof(Reducer<>)))
                {
                    throw new ArgumentException(
                        string.Format(CultureInfo.CurrentCulture, Properties.Resources.INVALID_REDUCER_ERROR, reducerkvp.Key));
                }

                // Create and store MethodInfo objects for reducers. This way, the performance hit is
                // minimal when combined reducer is called.
                MethodInfo componentReducerInfo = ((dynamic)reducerkvp.Value).Method;
                invokerMapping.Add(reducerkvp.Key, componentReducerInfo);
            }

            // Return a reducer which, when called, invokes the individual reducers in turn.
            return (state, action) =>
            {
                var nextState = new CombinedState(reducerMapping);

                if (state == null)
                {
                    state = nextState;
                }

                bool stateChanged = false;

                foreach (var reducerkvp in reducerMapping)
                {
                    dynamic componentState = state[reducerkvp.Key];
                    dynamic nextComponentState = invokerMapping[reducerkvp.Key].Invoke(null, new object[] { componentState, action });

                    if (componentState != nextComponentState)
                    {
                        stateChanged = true;
                        nextState[reducerkvp.Key] = nextComponentState;
                    }
                }

                return (stateChanged ? nextState : state);
            };
        }
    }
}
