using System;

namespace Redux
{
    public static class Redux
    {
        /// <summary>
        /// Creates a Redux store that serves a given type T.
        /// </summary>
        /// <typeparam name="T">Type of the reduced state.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>The created Redux store.</returns>
        public static IStore CreateStore<T>(Reducer<T> reducer)
        {
            throw new NotImplementedException();
        }
    }
}
