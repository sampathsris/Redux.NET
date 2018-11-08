using Redux;

namespace CounterExampleCore
{
    public class Counter: IReducer<int>
    {
        const string INCREMENT = "INCREMENT";
        const string DECREMENT = "DECREMENT";
        const string CHANGE_BY = "CHANGE_BY";

        static readonly ReduxAction ACTION_INCREMENT = new ReduxAction(INCREMENT);
        static readonly ReduxAction ACTION_DECREMENT = new ReduxAction(DECREMENT);

        public static ReduxAction Increment()
        {
            return ACTION_INCREMENT;
        }

        public static ReduxAction Decrement()
        {
            return ACTION_DECREMENT;
        }

        public static ReduxAction ChangeBy(int amount)
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
                return new ReduxAction<int>(CHANGE_BY, amount);
            }
        }

        public int Reduce(int state, ReduxAction action)
        {
            switch (action.ActionType)
            {
                case INCREMENT:
                    return state + 1;
                case DECREMENT:
                    return state - 1;
                case CHANGE_BY:
                    return state + (action as ReduxAction<int>).Payload;
                default:
                    return state;
            }
        }
    }
}
