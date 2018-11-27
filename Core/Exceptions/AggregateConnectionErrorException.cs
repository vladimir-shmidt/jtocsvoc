using System;
using System.Collections.Generic;

namespace Core
{
    [Serializable]
    public class AggregateConnectionErrorException : AggregateException
    {
        public AggregateConnectionErrorException() : base() { }
        public AggregateConnectionErrorException(IEnumerable<Exception> innerExceptions) : base(innerExceptions) { }
        public AggregateConnectionErrorException(params Exception[] innerExceptions) : base(innerExceptions) { }
        public AggregateConnectionErrorException(string message) : base(message) { }
        public AggregateConnectionErrorException(string message, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions) { }
        public AggregateConnectionErrorException(string message, Exception innerException) : base(message, innerException) { }
        public AggregateConnectionErrorException(string message, params Exception[] innerExceptions) : base(message, innerExceptions) { }
    }

}
