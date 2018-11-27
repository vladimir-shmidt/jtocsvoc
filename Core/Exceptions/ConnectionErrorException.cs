using System;
using System.Runtime.Serialization;

namespace Core
{
    [Serializable]
    public class ConnectionErrorException : DataConverterException
    {
        public ConnectionErrorException() { }
        public ConnectionErrorException(string message) : base(message) { }
        public ConnectionErrorException(string message, Exception inner) : base(message, inner) { }
        protected ConnectionErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
