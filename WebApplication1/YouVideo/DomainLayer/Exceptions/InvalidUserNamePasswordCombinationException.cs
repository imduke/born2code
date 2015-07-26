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
    public class InvalidUserNamePasswordCombinationException : YouVideosBaseException
    {
        public InvalidUserNamePasswordCombinationException() { }
        public InvalidUserNamePasswordCombinationException(string message) : base(message) { }
        public InvalidUserNamePasswordCombinationException(string message, Exception inner) : base(message, inner) { }
        protected InvalidUserNamePasswordCombinationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
