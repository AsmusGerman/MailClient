using System;
using System.Runtime.Serialization;

namespace MailClient.BLL
{
    [Serializable]
    internal class ExistentAccountException : Exception
    {
        public ExistentAccountException()
        {
        }

        public ExistentAccountException(string message) : base(message)
        {
        }

        public ExistentAccountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExistentAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}