using System;
using System.Collections.Generic;
using CounterExampleCore;
using ToDoListExampleCore;
using Redux;

namespace CombinedApp
{
    class Program
    {
        private static IStore combinedStore = null;

        static void Main(string[] args)
        {
            var counterReducer = new Counter();
            var toDoListReducer = new ToDoList();

            var combinedReducer = Ops.CombineReducers(new Dictionary<string, object>()
            {
                { "counter", counterReducer },
                { "todos",   toDoListReducer }
            });

            combinedStore = Ops.CreateStore<CombinedState>(combinedReducer);
            combinedStore.Subscribe<CombinedState>(CombinedStateChanged);

            Console.WriteLine("Initial state: " + combinedStore.GetState<CombinedState>());

            SendAction(Counter.Increment()); // 1
            SendAction(Counter.Decrement()); // 0
            SendAction(Counter.Increment()); // 1
            SendAction(Counter.Increment()); // 2

            // Shouldn't output anything.
            SendAction(new Redux.Action("DUMMY_ACTION"));

            SendAction(Counter.ChangeBy(-3)); // -1
            SendAction(Counter.ChangeBy(5));  // 4

            string a = "Learn Redux";
            string b = "Play with Redux";
            string c = "Implement Redux in .NET";

            SendAction(ToDoList.AddTodo(a));
            SendAction(ToDoList.AddTodo(b));
            SendAction(ToDoList.AddTodo(c));

            SendAction(ToDoList.ToggleTodo(a));
            SendAction(ToDoList.ToggleTodo(b));
            SendAction(ToDoList.ToggleTodo(b));
            Console.ReadKey();
        }

        private static void CombinedStateChanged(IStore store, CombinedState state)
        {
            Console.WriteLine(state);
        }

        private static void SendAction(Redux.Action action)
        {
            Console.WriteLine("Dispatching action: " + action);
            combinedStore.Dispatch(action);
        }
    }
}
