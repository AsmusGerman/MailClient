﻿using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class LoginException : WellKnownException
	{
		public LoginException()
		{
		}

		public LoginException(string message) : base(message)
		{
		}

		public LoginException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
