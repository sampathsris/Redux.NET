
namespace CounterExampleCore
{
    public static class Counter
    {
        const string INCREMENT = "INCREMENT";
        const string DECREMENT = "DECREMENT";
        const string CHANGE_BY = "CHANGE_BY";

        static readonly Redux.Action ACTION_INCREMENT = new Redux.Action(INCREMENT);
        static readonly Redux.Action ACTION_DECREMENT = new Redux.Action(DECREMENT);

        public static Redux.Action Increment()
        {
            return ACTION_INCREMENT;
        }

        public static Redux.Action Decrement()
        {
            return ACTION_DECREMENT;
        }

        public static Redux.Action ChangeBy(int amount)
        {
            if (amount == 1)
            {
                return Increment();
            }
            else if (amount == -1)
            {
                return Decrement();
            }
            else
            {
                return new Redux.Action<int>(CHANGE_BY, amount);
            }
        }

        public static int Reduce(int state, Redux.Action action)
        {
            switch (action.Type)
            {
                case INCREMENT:
                    return state + 1;
                case DECREMENT:
                    return state - 1;
                case CHANGE_BY:
                    return state + (action as Redux.Action<int>).Payload;
                default:
                    return state;
            }
        }
    }
}
