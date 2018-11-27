using System;
using System.Runtime.Serialization;

namespace Core
{
    [Serializable]
    public class FileWriteDataConverterException : DataConverterException
    {
        public FileWriteDataConverterException() { }
        public FileWriteDataConverterException(string message) : base(message) { }
        public FileWriteDataConverterException(string message, Exception inner) : base(message, inner) { }
        protected FileWriteDataConverterException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
