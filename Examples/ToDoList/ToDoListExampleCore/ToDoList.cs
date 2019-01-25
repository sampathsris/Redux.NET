using Redux;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoListExampleCore
{
    public class ToDoList: IState
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

        public static IState Reduce(IState state, ReduxAction action)
        {
            if (state == null)
            {
                return new ToDoList();
            }

            var prevList = state as ToDoList;
            string todo = null;

            if (action.ActionType == ADD_TODO || action.ActionType == TOGGLE_TODO)
            {
                todo = (action as ReduxAction<string>).Payload;
            }

            switch (action.ActionType)
            {
                case ADD_TODO:
                    return prevList.Add(todo, false);
                case TOGGLE_TODO:
                    return prevList.Toggle(todo);
                default:
                    return prevList;
            }
        }

        // The following code is a monstrosity. Must find a way to efficiently
        // handle immutable objects.
        private IDictionary<string, bool> todos = new Dictionary<string, bool>();

        public ToDoList() { }

        private ToDoList(ToDoList other)
        {
            this.todos = new Dictionary<string, bool>(other.todos);
        }

        public ToDoList Add(string todo, bool completed)
        {
            var newList = new ToDoList(this);
            newList.todos.Add(todo, completed);
            return newList;
        }

        public ToDoList Toggle(string todo)
        {
            var newList = new ToDoList(this);
            var prevCompleted = newList.todos[todo];
            newList.todos[todo] = !prevCompleted;
            return newList;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, todos.Select(
                kvp => (kvp.Value ? "[x]" : "[ ]") + " " + kvp.Key)
            );
        }
    }
}
