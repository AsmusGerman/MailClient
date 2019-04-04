using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class MCCoreInstantiateException : WellKnownException
	{
		public MCCoreInstantiateException()
		{
		}

		public MCCoreInstantiateException(string message) : base(message)
		{
		}

		public MCCoreInstantiateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
