
namespace Redux
{
    /// <summary>
    /// A function that is a Redux reducer.
    /// </summary>
    /// <typeparam name="T">Type of the reduced state.</typeparam>
    /// <param name="state">State before the reduction.</param>
    /// <param name="action">Action used for the reduction.</param>
    /// <returns>State after the reduction.</returns>
    public delegate T Reducer<T>(T state, Action action);
}
