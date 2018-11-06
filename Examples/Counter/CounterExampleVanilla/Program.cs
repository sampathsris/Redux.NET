using CounterExampleCore;
using Redux;
using System;

namespace CounterExample
{
    class Program
    {
        private static IStore counterStore = null;

        static void Main(string[] args)
        {
            counterStore = Redux.Ops.CreateStore<int>(new Counter());
            Redux.Ops.Subscribe<int>(counterStore, CounterStoreStateChanged);

            Console.WriteLine("Initial state: " + Redux.Ops.GetState<int>(counterStore)); // 0

            SendAction(Counter.Increment()); // 1
            SendAction(Counter.Decrement()); // 0
            SendAction(Counter.Increment()); // 1
            SendAction(Counter.Increment()); // 2

            // Shouldn't output anything.
            SendAction(new Redux.Action("DUMMY_ACTION"));

            SendAction(Counter.ChangeBy(-3)); // -1
            SendAction(Counter.ChangeBy(5));  // 4

            Console.ReadKey();
        }

        private static void CounterStoreStateChanged(IStore store, int state)
        {
            Console.WriteLine("State: " + state);
        }

        private static void SendAction(Redux.Action action)
        {
            Console.WriteLine("Dispatching action: " + action);
            counterStore.Dispatch(action);
        }
    }
}
