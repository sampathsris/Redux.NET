using CounterExampleCore;
using Redux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunkedCounter
{
    class Program
    {
        private static IStore counterStore = null;

        static void Main(string[] args)
        {
            var thunkMiddleware = StandardMiddleware.CreateThunkMiddleware<int>();
            var enhancer = Ops.ApplyMiddleware<int>(thunkMiddleware);
            var counterReducer = new Counter();
            counterStore = Ops.CreateStore<int>(counterReducer, () => 101, enhancer);
            counterStore.Subscribe<int>(CountChanged);

            ReduxThunk<int> incrementIfOdd = (dispatch, getState) =>
                {
                    var count = getState();
                    Console.WriteLine("incrementIfOdd called. counter: {0}", count);

                    if (count % 2 != 0)
                    {
                        dispatch(Counter.Increment());
                    }
                };
            var incrementIfOddAction = new ThunkAction<int>("INCREMENT_IF_ODD", incrementIfOdd);

            Console.WriteLine("Initial state: " + counterStore.GetState<int>()); // 0

            counterStore.Dispatch(incrementIfOddAction); // 102
            counterStore.Dispatch(incrementIfOddAction); // 102, unchanged.

            Console.ReadKey();
        }

        private static void CountChanged(IStore store, int state)
        {
            Console.WriteLine("counter: {0}", state);
        }
    }
}
