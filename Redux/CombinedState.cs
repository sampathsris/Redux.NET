using System;
using System.Collections.Generic;
using System.Linq;

namespace Redux
{
    /// <summary>
    /// Represents the state handled by a combined reducer.
    /// </summary>
    public class CombinedState
    {
        private IDictionary<string, object> componentStates;

        private CombinedState() { }

        internal CombinedState(IDictionary<string, object> componentMapping)
        {
            componentStates = new Dictionary<string, object>();

            foreach (var mapping in componentMapping)
            {
                // The type of the component states could either be reference or value
                // types. For value types, we need to initialize the state to said value
                // type's default value.
                Type t = mapping.Value
                    .GetType()                                          // Get the type of the value,
                    .GetInterfaces()                                    // ... get the implemented interfaces,
                    .Where(t1 =>                                        // ... filter by:
                        t1.IsGenericType &&                             // generic interfaces, and
                        t1.GetGenericTypeDefinition() == typeof(IReducer<>)) // ... check if the generic interface is IReducer
                    .ElementAt(0)                                       // This should return only 1 element. Get it.
                    .GetGenericArguments()[0];                          // IReducer has only 1 generic argument. Get it.

                componentStates.Add(mapping.Key, t.IsValueType ? Activator.CreateInstance(t) : null);
            }
        }
        
        internal object this[string component]
        {
            get { return componentStates[component]; }
            set { componentStates[component] = value; }
        }

        /// <summary>
        /// Gets the state of a given component from the CombinedState.
        /// </summary>
        /// <typeparam name="T">Type of the component state.</typeparam>
        /// <param name="component">Component state's name.</param>
        /// <returns>Component state.</returns>
        public T GetComponentState<T>(string component)
        {
            return (T)componentStates[component];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                componentStates.Select(kvp => (kvp.Key + ": " + kvp.Value)));
        }
    }
}
