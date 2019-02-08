namespace MailClient.Shared
{
	public interface ICommunicationProtocol
	{
		void Configure(string pHostName, int pPort, bool pSSL);
	}
}
