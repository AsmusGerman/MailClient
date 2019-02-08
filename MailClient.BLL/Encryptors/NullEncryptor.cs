using MailClient.Shared;

namespace MailClient.BLL
{
	public class NullEncryptor : IEncryptor
	{
		public string Decrypt(string pString)
		{
			return pString;
		}

		public string Encrypt(string pString)
		{
			return pString;
		}
	}
}
