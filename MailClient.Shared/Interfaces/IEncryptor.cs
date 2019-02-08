namespace MailClient.Shared
{
	public interface IEncryptor
	{
		string Encrypt(string pString);
		string Decrypt(string pString);
	}
}
