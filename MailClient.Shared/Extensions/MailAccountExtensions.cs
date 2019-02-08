using MailClient.Shared;
using System.Linq;

namespace MailClient_Shared.Extensions
{
	public static class MailAccountExtensions
	{
		public static string GetMailServiceHost(this MailAccount pMailAccount)
		{
			return new System.Net.Mail.MailAddress(pMailAccount.MailAddress.Value).Host.Split('.').First().ToLower();
		}
	}
}
