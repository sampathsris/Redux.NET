using CounterExampleCore;
using Redux;
using System;

namespace CounterExamplePreloaded
{
    class Program
    {
        private static IStore counterStore = null;

        static void Main(string[] args)
        {
            var loggerMiddleware = StandardMiddleware.CreateStdoutLoggerMiddleware<int>();
            var enhancer = Ops.ApplyMiddleware<int>(loggerMiddleware);
            var counterReducer = new Counter();
            // Create the store with preloaded value of 100.
            counterStore = Ops.CreateStore<int>(counterReducer, () => 100, enhancer);

            Console.WriteLine("Initial state: " + counterStore.GetState<int>()); // 0

            counterStore.Dispatch(Counter.Increment()); // 101
            counterStore.Dispatch(Counter.Decrement()); // 100
            counterStore.Dispatch(Counter.Increment()); // 101
            counterStore.Dispatch(Counter.Increment()); // 102

            // Shouldn't output anything.
            counterStore.Dispatch(new ReduxAction("DUMMY_ACTION"));

            counterStore.Dispatch(Counter.ChangeBy(-3)); //  99
            counterStore.Dispatch(Counter.ChangeBy(5));  // 104

            Console.ReadKey();
        }
    }
}
