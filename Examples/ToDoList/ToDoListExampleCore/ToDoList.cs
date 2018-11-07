using Redux;
using System.Collections.Generic;

namespace ToDoListExampleCore
{
    public class ToDoList : IReducer<IDictionary<string, bool>>
    {
        const string ADD_TODO = "ADD_TODO";
        const string TOGGLE_TODO = "TOGGLE_TODO";

        public static ReduxAction AddTodo(string todo)
        {
            return new ReduxAction<string>(ADD_TODO, todo);
        }

        public static ReduxAction ToggleTodo(string todo)
        {
            return new ReduxAction<string>(TOGGLE_TODO, todo);
        }

        public IDictionary<string, bool> Reduce(IDictionary<string, bool> state, ReduxAction action)
        {
            if (state == null)
            {
                return new Dictionary<string, bool>();
            }

            Dictionary<string, bool> newTodoList = null;
            string todo = null;

            if (action.Type == ADD_TODO || action.Type == TOGGLE_TODO)
            {
                todo = (action as ReduxAction<string>).Payload;
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
