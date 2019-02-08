using MailClient.Shared;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.BLL
{
	public static class MessageExtension
	{
		public static MailMessage ConvertToMailMessage(this Message pMessage)
		{

			System.Net.Mail.MailMessage mMailMessage = pMessage.ToMailMessage();

			return new MailMessage()
			{
				ExternalId = pMessage.Headers.MessageId,
				Body = mMailMessage.Body ?? default(string),
				Subject = mMailMessage.Subject ?? default(string),
				From = new MailAddress() { Value = mMailMessage.From.GetAddress() },
				To = pMessage.Headers.To.GetAddresses().Select(bMailAddress => new MailAddress() { Value = bMailAddress }).ToList()
			};
		}
		private static IEnumerable<string> GetAddresses(this IEnumerable<RfcMailAddress> pTo)
		{

			IEnumerable<string> mAddresses = new List<string>();

			if (pTo.Any())
				mAddresses = pTo.Where(_ => true).Select(bItem => bItem.Address).ToList();

			return mAddresses;
		}
		private static string GetAddress(this System.Net.Mail.MailAddress pMailAddress)
		{

			if (pMailAddress != default(System.Net.Mail.MailAddress))
				return pMailAddress.Address ?? default(string);

			return default(string);
		}
	}
}
