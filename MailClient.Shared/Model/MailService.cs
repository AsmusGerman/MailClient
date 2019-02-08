using System;
using System.Collections.Generic;

namespace MailClient.Shared
{
	[Serializable]
	public class MailService
	{
		public Protocol[] Protocols { get; set; }
	}
}
