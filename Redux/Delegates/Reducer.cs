
namespace Redux
{
    /// <summary>
    /// Represents a Redux reducer.
    /// </summary>
    /// <param name="state">State before the reduction.</param>
    /// <param name="action">Action used for the reduction.</param>
    /// <returns>State after the reduction.</returns>
    public delegate IState Reducer(IState state, ReduxAction action);
}
