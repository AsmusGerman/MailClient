using System.Collections.Generic;

namespace MailClient.Shared
{
	public interface IMailService
	{
		IEnumerable<MailMessage> Retrieve(string pMailAddress, string pPassword, int pOffset = 0, int pWindow = 0);
		void Send(string pMailAddress, string pPassword, MailMessage pMailMessage);
	}
}
