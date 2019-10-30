﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ServiceRepoDemo.Service.Errors
{

    [Serializable]
    public class AuthenticationException : Exception
    {
        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception innerException) : base(message, innerException) { }
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
