using CounterExampleCore;
using Redux;
using Redux.Primitives;
using System;

namespace CounterExample
{
    class Program
    {
        private static IStore<int> counterStore = null;

        static void Main(string[] args)
        {
            counterStore = Ops<int>.CreateStore(Counter.Reduce);
            counterStore.StateChanged += CounterStoreStateChanged;

            Console.WriteLine("Initial state: " + counterStore.State); // 0

            SendAction(Counter.Increment()); // 1
            SendAction(Counter.Decrement()); // 0
            SendAction(Counter.Increment()); // 1
            SendAction(Counter.Increment()); // 2

            // Shouldn't output anything.
            SendAction(new ReduxAction("DUMMY_ACTION"));

            SendAction(Counter.ChangeBy(-3)); // -1
            SendAction(Counter.ChangeBy(5));  // 4

            Console.ReadKey();
        }

        private static void CounterStoreStateChanged(object sender, int state)
        {
            Console.WriteLine("State: " + state);
        }

        private static void SendAction(ReduxAction action)
        {
            Console.WriteLine("Dispatching action: " + action);
            counterStore.Dispatch(action);
        }
    }
}
