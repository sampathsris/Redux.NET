using Redux;
using System;
using System.Collections.Generic;
using ToDoListExampleCore;

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

            todoListStore = Ops.CreateStore(ToDoList.Reduce);
            todoListStore.Subscribe(ToDoListStateChanged);

            SendAction(ToDoList.AddTodo(a));
            SendAction(ToDoList.AddTodo(b));
            SendAction(ToDoList.AddTodo(c));

            SendAction(ToDoList.ToggleTodo(a));
            SendAction(ToDoList.ToggleTodo(b));
            SendAction(ToDoList.ToggleTodo(b));
            Console.ReadKey();
        }

        private static void ToDoListStateChanged(IStore store, IState state)
        {
            Console.WriteLine(state);
        }

        private static void SendAction(ReduxAction action)
        {
            Console.WriteLine("Dispatching action: " + action);
            todoListStore.Dispatch(action);
        }
    }
}
