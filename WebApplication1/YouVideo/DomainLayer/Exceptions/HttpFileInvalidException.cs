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
    public class HttpFileInvalidException : YouVideosBaseException
    {
        public HttpFileInvalidException() { }
        public HttpFileInvalidException(string message) : base(message) { }
        public HttpFileInvalidException(string message, Exception inner) : base(message, inner) { }
        protected HttpFileInvalidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
