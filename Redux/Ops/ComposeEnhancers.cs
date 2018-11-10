using System.Linq;

namespace Redux
{
    public static partial class Ops
    {
        /// <summary>
        /// Composes multiple store enhancers into a single enhancer.
        /// </summary>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="enhancerChain">A list of enhancers, such as the function
        /// returned from ApplyMiddleware.</param>
        /// <returns>Composed enhancer.</returns>
        public static StoreEnhancer<TState> ComposeEnhancers<TState>(
            params StoreEnhancer<TState>[] enhancerChain)
        {
            // Similar to ComposeMiddleware.
            return (storeCreator) => enhancerChain
                .Reverse()
                .Aggregate<StoreEnhancer<TState>, StoreCreator<TState>>(
                    storeCreator,
                    (acc, func) => func(acc)
                );
        }
    }
}
