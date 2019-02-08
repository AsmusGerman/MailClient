using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class ExistingMailAccount : WellKnownException
	{
		public ExistingMailAccount()
		{
		}

		public ExistingMailAccount(string message) : base(message)
		{
		}

		public ExistingMailAccount(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
