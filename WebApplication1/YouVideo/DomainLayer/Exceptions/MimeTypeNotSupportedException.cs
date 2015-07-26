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
    public class MimeTypeNotSupportedException : YouVideosBaseException
    {
        public MimeTypeNotSupportedException() { }
        public MimeTypeNotSupportedException(string message) : base(message) { }
        public MimeTypeNotSupportedException(string message, Exception inner) : base(message, inner) { }
        protected MimeTypeNotSupportedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
