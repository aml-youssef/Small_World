using System;
using System.Runtime.Serialization;

namespace Small_World
{
    [Serializable]
    internal class IllegalArgumentException : Exception
    {
        private object p;

        public IllegalArgumentException()
        {
        }

        public IllegalArgumentException(object p)
        {
            this.p = p;
        }

        public IllegalArgumentException(string message) : base(message)
        {
        }

        public IllegalArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IllegalArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}