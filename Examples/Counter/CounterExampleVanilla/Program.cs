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
            counterStore = Redux.Redux.CreateStore<int>(Counter.Reduce);

            Console.WriteLine("Initial state: " + Redux.Redux.GetState<int>(counterStore)); // 0

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

        private static void SendAction(Redux.Action action)
        {
            Console.WriteLine("Dispatching action: " + action);
            counterStore.Dispatch(action);
            Console.WriteLine("State: " + Redux.Redux.GetState<int>(counterStore));
        }
    }
}
