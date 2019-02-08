using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class UnknownMailAccount : WellKnownException
	{
		public UnknownMailAccount()
		{
		}

		public UnknownMailAccount(string message) : base(message)
		{
		}

		public UnknownMailAccount(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
