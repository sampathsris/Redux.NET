using CounterExampleCore;
using Redux;
using Redux.Primitives;
using System;

namespace CounterExampleWithLogger
{
    class Program
    {
        private static IStore<int> counterStore = null;

        static void Main(string[] args)
        {
            var loggerMiddleware = StandardMiddleware.CreateStdoutLoggerMiddleware();
            var enhancer = Redux.Ops.ApplyMiddleware(loggerMiddleware);
            counterStore = Ops<int>.CreateStore(Counter.Reduce, 0, enhancer);
            counterStore.StateChanged += CounterStoreStateChanged;

            Console.WriteLine("Initial state: " + counterStore.State); // 0

            counterStore.Dispatch(Counter.Increment()); // 1
            counterStore.Dispatch(Counter.Decrement()); // 0
            counterStore.Dispatch(Counter.Increment()); // 1
            counterStore.Dispatch(Counter.Increment()); // 2

            // Shouldn't output anything.
            counterStore.Dispatch(new ReduxAction("DUMMY_ACTION"));

            counterStore.Dispatch(Counter.ChangeBy(-3)); // -1
            counterStore.Dispatch(Counter.ChangeBy(5));  // 4

            Console.ReadKey();
        }

        private static void CounterStoreStateChanged(object sender, int state)
        {
            Console.WriteLine("CounterStoreStateChanged: {0}", state);
        }
    }
}
