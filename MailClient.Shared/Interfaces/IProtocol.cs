namespace MailClient.Shared
{
	public interface IProtocol
	{
		string Host { get; set; }
		string Name { get; set; }
		int Port { get; set; }
		bool SSL { get; set; }
	}
}