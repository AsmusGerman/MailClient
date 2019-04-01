using MailClient.Shared;

namespace MailClient.Core
{
	public interface IAuthenticationService
	{
		void Register(string pAlias, string pMailAddress, string pPassword);
		bool LoginByAlias(string pAlias, string pPassword);
		bool LoginByMailAddress(string pMailAddress, string pPassword);
	}
}
