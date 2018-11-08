
namespace Redux
{
    /// <summary>
    /// An event handler that handles a StateChanged event of a store.
    /// </summary>
    /// <typeparam name="T">Type of the state.</typeparam>
    /// <param name="store">The store that publishes the event.</param>
    /// <param name="state">State of the store that publishes the event.</param>
    public delegate void StateChangedEventHandler<T>(
        IStore store,
        T state
    );

    /// <summary>
    /// Represents a function that can create a Redux store.
    /// </summary>
    /// <typeparam name="T">Type of the stored state.</typeparam>
    /// <param name="reducer">Reducer used on the state.</param>
    /// <returns>A redux store.</returns>
    public delegate IStore StoreCreator<T>(
        IReducer<T> reducer,
        StoreEnhancer<T> enhancer = null
    );

    /// <summary>
    /// Represents a function that accepts a StoreCreator, enhances it,
    /// and returns a new store creator.
    /// </summary>
    /// <typeparam name="T">Type of the stored state.</typeparam>
    /// <param name="storeCreator">StoreCreator to be enhanced.</param>
    /// <returns>Enhanced StoreCreator.</returns>
    public delegate StoreCreator<T> StoreEnhancer<T>(
        StoreCreator<T> storeCreator
    );
}
