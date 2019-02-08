using MailClient.Shared;
using System.Collections.Generic;

namespace MailClient.Core
{
    public interface IServicedMailAccountService
    {
        IMailAccountService With(MailService pMailService);
    }
    public interface IMailAccountService : IServicedMailAccountService
	{
        IEnumerable<MailMessage> Retrieve(MailAccount pMailAccount, int pWindow = 0);
		void Send(MailAccount pMailAccount, MailMessage pMailMessage);
	}
}
