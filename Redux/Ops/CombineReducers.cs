using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Dynamic;

namespace Redux
{
    public static partial class Ops
    {
        /*
        /// <summary>
        /// Combines a list of reducers into a single reducer.
        /// </summary>
        /// <param name="reducers">
        /// An array or parameter list of reducers. All reducers should be of type
        /// <code>Redux.Reducer</code>.
        /// </param>
        /// <returns>A combined reducer.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static Reducer CombineReducers(IDictionary<string, Reducer> reducerMapping)
        {
            if (reducerMapping == null) throw new ArgumentNullException("reducerMapping");

            IDictionary<string, MethodInfo> invokerMapping = new Dictionary<string, MethodInfo>();

            foreach (var reducerkvp in reducerMapping)
            {
                Type type = reducerkvp.Value.GetType();

                // TODO: Now that we have got rid of generic reducers thans to IState,
                // we have to revise this code.
                if (!(type.IsGenericType ||
                      type.GetGenericTypeDefinition() == typeof(Reducer)))
                {
                    throw new ArgumentException(
                        string.Format(CultureInfo.CurrentCulture, Properties.Resources.INVALID_REDUCER_ERROR, reducerkvp.Key));
                }

                // Create and store MethodInfo objects for reducers. This way, the performance hit is
                // minimal when combined reducer is called.
                MethodInfo componentReducerInfo = ((dynamic)reducerkvp.Value).Method;
                invokerMapping.Add(reducerkvp.Key, componentReducerInfo);
            }

            var initializeState = CreateCombinedStateInitializer(reducerMapping);

            // Return a reducer which, when called, invokes the individual reducers in turn.
            return (state, action) =>
            {
                var nextState = initializeState(state);

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

        private static Func<IDictionary<string, dynamic>, IDictionary<string, dynamic>> CreateCombinedStateInitializer(IDictionary<string, object> reducerMapping)
        {
            var states = new Dictionary<string, dynamic>();

            foreach (var reducerkvp in reducerMapping)
            {
                // The type of the component states could either be reference or value
                // types. For value types, we need to initialize the state to said value
                // type's default value.
                //
                // Reducer has only 1 generic argument.
                Type t = reducerkvp.Value.GetType().GetGenericArguments()[0];
                states.Add(reducerkvp.Key, t.IsValueType ? Activator.CreateInstance(t) : null);
            }

            return (prevState) => new Dictionary<string, dynamic>(prevState == null ? states : prevState);
        }
        */
    }
}
