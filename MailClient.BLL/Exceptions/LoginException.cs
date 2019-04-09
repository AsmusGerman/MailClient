using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL
{
	public class UnknownAccountException : Exception
	{
		public UnknownAccountException()
		{
		}

		public UnknownAccountException(string message) : base(message)
		{
		}

		public UnknownAccountException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
