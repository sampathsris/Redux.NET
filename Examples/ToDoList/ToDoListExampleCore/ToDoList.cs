using Redux;
using System.Collections.Generic;

namespace ToDoListExampleCore
{
    public static class ToDoList
    {
        const string ADD_TODO = "ADD_TODO";
        const string TOGGLE_TODO = "TOGGLE_TODO";

        public static Action AddTodo(string todo)
        {
            return new Action<string>(ADD_TODO, todo);
        }

        public static Action ToggleTodo(string todo)
        {
            return new Action<string>(TOGGLE_TODO, todo);
        }

        public static IDictionary<string, bool> Reduce(IDictionary<string, bool> state, Action action)
        {
            if (state == null)
            {
                return new Dictionary<string, bool>();
            }

            Dictionary<string, bool> newTodoList = null;
            string todo = null;

            if (action.Type == ADD_TODO || action.Type == TOGGLE_TODO)
            {
                todo = (action as Redux.Action<string>).Payload;
            }

            switch (action.Type)
            {
                case ADD_TODO:
                    newTodoList = new Dictionary<string, bool>(state);
                    newTodoList.Add(todo, false);
                    return newTodoList;
                case TOGGLE_TODO:
                    newTodoList = new Dictionary<string, bool>();
                    foreach (var item in state)
                    {
                        bool value = (item.Key == todo) ? (!item.Value) : item.Value;
                        newTodoList.Add(item.Key, value);
                    }
                    return newTodoList;
                default:
                    return state;
            }
        }
    }
}
