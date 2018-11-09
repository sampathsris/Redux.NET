using System;
using System.Runtime.Serialization;

namespace Redux
{
    /// <summary>
    /// Represents a Redux action with no accompanying payload.
    /// </summary>
    [Serializable]
    public class ReduxAction
    {
        /// <summary>
        /// Action used to initilize every redux store.
        /// </summary>
        internal static readonly ReduxAction __INIT__ = new ReduxAction("__INIT__");

        /// <summary>
        /// Every Redux action has a mandatory Type property.
        /// </summary>
        public string ActionType
        {
            get;
            // Only the constructor may set the action's Type during initialization.
            private set;
        }

        /// <summary>
        /// Creates an Action object.
        /// </summary>
        /// <param name="type">Type of the Redux action.</param>
        public ReduxAction(string type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "Type must not be null when creating an Action.");
            }

            ActionType = type;
        }

        public override string ToString()
        {
            return "ReduxAction, Type: " + ActionType;
        }

        public void GetObjectData(SerializationInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            info.AddValue("Type", ActionType, typeof(string));
        }

        protected ReduxAction(SerializationInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            ActionType = info.GetString("Type");
        }
    }

    /// <summary>
    /// A generic action that can carry a payload of a given type.
    /// 
    /// <b>Note: </b>This class is marked with the <code>SystemSerilizableAttribute</code>
    /// in order to enable serialization. However, its true ability to serialize depends
    /// on the serializability of the Payload. There is not compile time or runtime type
    /// safety checks to ensure serializability of Paylaod, and this is done to keep the
    /// class flexible such that non-serializable types can be used as a payload. If you
    /// must serialize actions (for example, to save a list of actions to a file), then
    /// you must use a serializable type for Payload.
    /// </summary>
    /// <typeparam name="TState">Type of payload</typeparam>
    [Serializable]
    public class ReduxAction<TState> : ReduxAction
    {
        /// <summary>
        /// Other data associated with the action, apart from action's Type.
        /// </summary>
        public TState Payload { get; private set; }

        public ReduxAction(string type, TState payload)
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
