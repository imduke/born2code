using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ServiceLocatorConfigurationInvalidException : YouVideosBaseException
    {
        public ServiceLocatorConfigurationInvalidException() { }
        public ServiceLocatorConfigurationInvalidException(string message) : base(message) { }
        public ServiceLocatorConfigurationInvalidException(string message, Exception inner) : base(message, inner) { }
        protected ServiceLocatorConfigurationInvalidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
