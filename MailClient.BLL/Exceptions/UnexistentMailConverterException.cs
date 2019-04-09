using System;
using System.Runtime.Serialization;

namespace MailClient.BLL
{
    [Serializable]
    internal class UnexistentMailConverterException : Exception
    {
        public UnexistentMailConverterException()
        {
        }

        public UnexistentMailConverterException(string message) : base(message)
        {
        }

        public UnexistentMailConverterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexistentMailConverterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}