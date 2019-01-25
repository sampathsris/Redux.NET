using CounterExampleCore;
using Redux;
using System;

namespace CounterExamplePreloaded
{
    class Program
    {
        private static PrimitiveStore<int> counterStore = null;

        static void Main(string[] args)
        {
            var loggerMiddleware = StandardMiddleware.CreateStdoutLoggerMiddleware();
            var enhancer = Ops.ApplyMiddleware(loggerMiddleware);
            // Create the store with preloaded value of 100.
            counterStore = Ops.CreateStore<int>(Counter.Reduce, 100, enhancer);

            Console.WriteLine("Initial state: " + counterStore.State); // 0

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
