using System;
using System.Runtime.Serialization;

namespace Core
{
    [Serializable]
    public class DataConverterException : Exception
    {
        public DataConverterException() { }
        public DataConverterException(string message) : base(message) { }
        public DataConverterException(string message, Exception inner) : base(message, inner) { }
        protected DataConverterException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
