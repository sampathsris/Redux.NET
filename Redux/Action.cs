using System;

namespace Redux
{
    /// <summary>
    /// Represents a Redux action with no accompanying payload.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Action used to initilize every redux store.
        /// </summary>
        internal static readonly Action __INIT__ = new Action("__INIT__");

        /// <summary>
        /// Every Redux action has a mandatory Type property.
        /// </summary>
        public string Type
        {
            get;
            // Only the constructor may set the action's Type during initialization.
            private set;
        }

        /// <summary>
        /// Creates an Action object.
        /// </summary>
        /// <param name="type">Type of the Redux action.</param>
        public Action(string type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Type", "Type must not be null when creating an Action.");
            }

            Type = type;
        }

        public override string ToString()
        {
            return "Redux.Action, Type: " + Type;
        }
    }

    /// <summary>
    /// A generic action that can carry a payload of a given type.
    /// </summary>
    /// <typeparam name="T">Type of payload</typeparam>
    public class Action<T> : Action
    {
        /// <summary>
        /// Other data associated with the action, apart from action's Type.
        /// </summary>
        public T Payload { get; private set; }

        public Action(string type, T payload)
            : base(type)
        {
            Payload = payload;
        }

        public override string ToString()
        {
            return base.ToString() + ", Payload: " + Payload;
        }
    }
}
