using Redux;
using System;
using System.Linq;
using ToDoListExampleCore;

using ToDoObj = System.Collections.Generic.IDictionary<string, bool>;

namespace ToDoListExample
{
    class Program
    {
        private static IStore todoListStore = null;

        static void Main(string[] args)
        {
            string a = "Learn Redux";
            string b = "Play with Redux";
            string c = "Implement Redux in .NET";

            todoListStore = Ops.CreateStore<ToDoObj>(new ToDoList());
            todoListStore.Subscribe<ToDoObj>(ToDoListStateChanged);

            SendAction(ToDoList.AddTodo(a));
            SendAction(ToDoList.AddTodo(b));
            SendAction(ToDoList.AddTodo(c));

            SendAction(ToDoList.ToggleTodo(a));
            SendAction(ToDoList.ToggleTodo(b));
            SendAction(ToDoList.ToggleTodo(b));
            Console.ReadKey();
        }

        private static void ToDoListStateChanged(IStore store, ToDoObj state)
        {
            Console.WriteLine(string.Join(Environment.NewLine, state.Select(
                kvp => (kvp.Value ? "[x]" : "[ ]") + " " + kvp.Key)
            ));
        }

        private static void SendAction(Redux.Action action)
        {
            Console.WriteLine("Dispatching action: " + action);
            todoListStore.Dispatch(action);
        }
    }
}
