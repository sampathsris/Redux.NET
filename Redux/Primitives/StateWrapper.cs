
namespace Redux.Primitives
{
    internal class StateWrapper<TState> : IState
    {
        public StateWrapper(TState payload)
        {
            Payload = payload;
        }

        public TState Payload { get; set; }

        public override string ToString()
        {
            return Payload.ToString();
        }
    }
}
