using System;
using System.Runtime.Serialization;

namespace MailClient.BLL
{
    [Serializable]
    internal class AccountDeletedException : Exception
    {
        public AccountDeletedException()
        {
        }

        public AccountDeletedException(string message) : base(message)
        {
        }

        public AccountDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}