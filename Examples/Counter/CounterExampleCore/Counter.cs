using Redux;

namespace CounterExampleCore
{
    public class Counter: IReducer<int>
    {
        const string INCREMENT = "INCREMENT";
        const string DECREMENT = "DECREMENT";
        const string CHANGE_BY = "CHANGE_BY";

        static readonly Action ACTION_INCREMENT = new Action(INCREMENT);
        static readonly Action ACTION_DECREMENT = new Action(DECREMENT);

        public static Action Increment()
        {
            return ACTION_INCREMENT;
        }

        public static Action Decrement()
        {
            return ACTION_DECREMENT;
        }

        public static Action ChangeBy(int amount)
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
                return new Action<int>(CHANGE_BY, amount);
            }
        }

        public int Reduce(int state, Action action)
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
