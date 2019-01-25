
namespace Redux
{
    public static partial class Ops
    {
        /// <summary>
        /// Returns a reducer that is an identity function. In other words,
        /// it simply returns the previous state.
        /// </summary>
        /// <typeparam name="TState">Type of the reduced state.</typeparam>
        public static Reducer GetIdentityReducer()
        {
            return (state, action) => state;
        }
    }
}
