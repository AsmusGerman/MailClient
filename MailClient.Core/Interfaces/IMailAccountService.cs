using MailClient.Shared;
using System.Collections.Generic;

namespace MailClient.Core
{
    public interface IMailAccountService
	{
        IEnumerable<MailMessage> Retrieve(MailAccount pMailAccount, int pWindow = 0);
		void Send(MailAccount pMailAccount, MailMessage pMailMessage);
	}
}
