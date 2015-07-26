﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.DomainLayer.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class YouVideosBaseException : Exception
    {
        public YouVideosBaseException() { }
        public YouVideosBaseException(string message) : base(message) { }
        public YouVideosBaseException(string message, Exception inner) : base(message, inner) { }
        protected YouVideosBaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
