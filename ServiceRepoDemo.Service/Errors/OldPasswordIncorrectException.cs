using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ServiceRepoDemo.Service.Errors
{
  
    [Serializable]
    public class OldPasswordIncorrectException : Exception
    {
        public OldPasswordIncorrectException() { }
        public OldPasswordIncorrectException(string message) : base(message) { }
        public OldPasswordIncorrectException(string message, Exception innerException) : base(message, innerException) { }
        protected OldPasswordIncorrectException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
