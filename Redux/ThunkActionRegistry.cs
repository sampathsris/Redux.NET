using System;
using System.Collections.Generic;
using System.Globalization;

namespace Redux
{
    /// <summary>
    /// Stores implementations of thunk actions, and resolves them against `ThunkAction`s.
    /// </summary>
    public static class ThunkActionRegistry
    {
        private class TypedRegistry<TState>
        {
            private IDictionary<string, ReduxThunk<TState>> registry = new Dictionary<string, ReduxThunk<TState>>();

            public Action Register(string thunkName, ReduxThunk<TState> thunk)
            {
                if (registry.ContainsKey(thunkName))
                {
                    throw new ArgumentException(
                        string.Format(CultureInfo.CurrentCulture, "A thunk with the name '{0}' already exists.", thunkName));
                }

                registry.Add(thunkName, thunk);
                bool registered = true;

                return () =>
                {
                    if (registered)
                    {
                        registry.Remove(thunkName);
                        registered = false;
                    }
                };
            }

            public ReduxThunk<TState> Resolve(string thunkName)
            {
                ReduxThunk<TState> thunk;
                bool registered = registry.TryGetValue(thunkName, out thunk);

                if (!registered)
                {
                    throw new ArgumentException(
                        string.Format(CultureInfo.CurrentCulture, "A thunk with the name '{0}' is not registered.", thunkName));
                }

                return thunk;
            }
        }

        private static IDictionary<Type, object> registryRegistry = new Dictionary<Type, object>();

        private static TypedRegistry<T> GetTypedRegistry<T>()
        {
            Type type = typeof(T);
            object registry;
            bool registryExists = registryRegistry.TryGetValue(type, out registry);

            if (!registryExists)
            {
                registry = new TypedRegistry<T>();
                registryRegistry.Add(type, registry);
            }

            return (registry as TypedRegistry<T>);
        }

        /// <summary>
        /// Registers a thunk in the registry.
        /// </summary>
        /// <param name="thunkName">Name of the thunk.</param>
        /// <param name="thunk">A redux thunk.</param>
        /// <returns>A function to unregister the thunk.</returns>
        public static Action Register<T>(string thunkName, ReduxThunk<T> thunk)
        {
            if (thunkName == null)
            {
                throw new ArgumentNullException("thunkName");
            }

            if (thunk == null)
            {
                throw new ArgumentNullException("thunk");
            }

            return GetTypedRegistry<T>().Register(thunkName, thunk);
        }

        /// <summary>
        /// Finds a thunk registered with a given name, if it is registered. Throws otherwise.
        /// </summary>
        /// <param name="thunkName">Name of the thunk.</param>
        /// <returns>The registered thunk, if it exists.</returns>
        public static ReduxThunk<T> Resolve<T>(string thunkName)
        {
            return GetTypedRegistry<T>().Resolve(thunkName);
        }
    }
}
