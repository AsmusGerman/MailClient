using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class UpdateInboxException : WellKnownException
	{
		public UpdateInboxException()
		{
		}

		public UpdateInboxException(string message) : base(message)
		{
		}

		public UpdateInboxException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
