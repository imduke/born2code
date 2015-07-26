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
    public class VideoInfoInvalidException : YouVideosBaseException
    {
        public VideoInfoInvalidException() { }
        public VideoInfoInvalidException(string message) : base(message) { }
        public VideoInfoInvalidException(string message, Exception inner) : base(message, inner) { }
        protected VideoInfoInvalidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
