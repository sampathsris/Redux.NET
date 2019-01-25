using CounterExampleCore;
using Redux;
using Redux.Primitives;
using System;

namespace ThunkedCounter
{
    class Program
    {
        private static IStore<int> counterStore = null;

        static void Main(string[] args)
        {
            //var thunkMiddleware = StandardMiddleware.CreateThunkMiddleware();
            //var enhancer = Ops.ApplyMiddleware(thunkMiddleware);
            //counterStore = Ops.CreateStore<int>(Counter.Reduce, 101, enhancer);
            //counterStore.Subscribe<int>(CountChanged);

            //PrimitiveReduxThunk<int> incrementIfOdd = (dispatch, getState) =>
            //    {
            //        var count = getState();
            //        Console.WriteLine("incrementIfOdd called. counter: {0}", count);

            //        if (count % 2 != 0)
            //        {
            //            dispatch(Counter.Increment());
            //        }
            //    };
            //var incrementIfOddAction = new ThunkAction<int>("INCREMENT_IF_ODD", incrementIfOdd);

            //Console.WriteLine("Initial state: " + counterStore.StateWrapper); // 0

            //counterStore.Dispatch(incrementIfOddAction); // 102
            //counterStore.Dispatch(incrementIfOddAction); // 102, unchanged.

            //Console.ReadKey();
        }

        private static void CountChanged(IStore store, int state)
        {
            Console.WriteLine("counter: {0}", state);
        }
    }
}
