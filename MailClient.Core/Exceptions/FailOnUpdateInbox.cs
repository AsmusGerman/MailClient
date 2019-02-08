using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class FailOnUpdateInbox : WellKnownException
	{
		public FailOnUpdateInbox()
		{
		}

		public FailOnUpdateInbox(string message) : base(message)
		{
		}

		public FailOnUpdateInbox(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
