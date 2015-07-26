using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YouVideo.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class DataServiceUnknownException : Exception
    {
        public DataServiceUnknownException() { }
        public DataServiceUnknownException(string message) : base(message) { }
        public DataServiceUnknownException(string message, Exception inner) : base(message, inner) { }
        protected DataServiceUnknownException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
