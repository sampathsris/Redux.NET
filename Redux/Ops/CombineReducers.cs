using System;
using System.Linq;
using System.Collections.Generic;

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
        public static IReducer<CombinedState> CombineReducers(IDictionary<string, object> reducerMapping)
        {
            if (reducerMapping == null) throw new ArgumentNullException("reducerMapping");

            foreach (var reducerkvp in reducerMapping)
            {
                // Check if each of the reducers are valid reducers. We simply check if the
                // object implements IReducer<>.
                if (!reducerkvp.Value.GetType()
                    .GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IReducer<>)
                    ))
                {
                    throw new ArgumentException("Invalid Reducer.");
                }
            }

            return new CombinedReducer(reducerMapping);
        }
    }
}
