using System;
using System.Runtime.Serialization;

namespace AutoService.Models.CustomExceptions
{
    public class InvalidIdException : ArgumentException
    {
        public InvalidIdException()
        {
        }

        public InvalidIdException(string message) : base(message)
        {
        }

        public InvalidIdException(string message, string paramName) : base(message, paramName)
        {
        }

        public InvalidIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidIdException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected InvalidIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
