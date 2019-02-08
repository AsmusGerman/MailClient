using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class UnknownComunicationProtocol : WellKnownException
	{
		public UnknownComunicationProtocol()
		{
		}

		public UnknownComunicationProtocol(string message) : base(message)
		{
		}

		public UnknownComunicationProtocol(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
