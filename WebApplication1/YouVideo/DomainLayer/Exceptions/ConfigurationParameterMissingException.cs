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
    public class ConfigurationParameterMissingException : YouVideosBaseException
    {
        public ConfigurationParameterMissingException() { }
        public ConfigurationParameterMissingException(string message) : base(message) { }
        public ConfigurationParameterMissingException(string message, Exception inner) : base(message, inner) { }
        protected ConfigurationParameterMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
