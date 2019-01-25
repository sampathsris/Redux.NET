using System;
using System.Linq;
using System.Collections.Generic;
using CounterExampleCore;
using ToDoListExampleCore;
using Redux;

namespace CombinedApp
{
    using CombinedState = IDictionary<string, dynamic>;

    class Program
    {
        private static IStore combinedStore = null;

        static void Main(string[] args)
        {
            //var combinedReducer = Ops.CombineReducers(new Dictionary<string, object>()
            //{
            //    { "counter", (Reducer<int>)Counter.Reduce },
            //    { "todos",   (Reducer<IDictionary<string, bool>>)ToDoList.Reduce }
            //});

            //combinedStore = Ops.CreateStore<CombinedState>(combinedReducer);
            //combinedStore.Subscribe<CombinedState>(CombinedStateChanged);

            //Console.WriteLine("Initial state: " + combinedStore.GetState<CombinedState>());

            //SendAction(Counter.Increment()); // 1
            //SendAction(Counter.Decrement()); // 0
            //SendAction(Counter.Increment()); // 1
            //SendAction(Counter.Increment()); // 2

            //// Shouldn't output anything.
            //SendAction(new ReduxAction("DUMMY_ACTION"));

            //SendAction(Counter.ChangeBy(-3)); // -1
            //SendAction(Counter.ChangeBy(5));  // 4

            //string a = "Learn Redux";
            //string b = "Play with Redux";
            //string c = "Implement Redux in .NET";

            //SendAction(ToDoList.AddTodo(a));
            //SendAction(ToDoList.AddTodo(b));
            //SendAction(ToDoList.AddTodo(c));

            //SendAction(ToDoList.ToggleTodo(a));
            //SendAction(ToDoList.ToggleTodo(b));
            //SendAction(ToDoList.ToggleTodo(b));
            //Console.ReadKey();
        }

        private static void CombinedStateChanged(IStore store, CombinedState state)
        {
            Console.WriteLine(state.Select(kvp => kvp.Key + ": " + kvp.Value.ToString()));
        }

        private static void SendAction(ReduxAction action)
        {
            Console.WriteLine("Dispatching action: " + action);
            combinedStore.Dispatch(action);
        }
    }
}
