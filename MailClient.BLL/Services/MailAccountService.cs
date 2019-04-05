using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Shared;
using MailClient.DAL;
using MailClient.Core;
using MailClient.BLL.Selectors;

namespace MailClient.BLL
{
	public class MailAccountService : IMailAccountService
	{
		private IDictionary<string, Protocol> iComunicationProtocols;

		public IEnumerable<MailMessage> Retrieve(MailAccount pMailAccount, int pWindow = 0)
		{
			try
			{
				int mLastMessageId = pMailAccount.MailAddress.FromMessages.Any() ? pMailAccount.MailAddress.FromMessages.Max(bMessage => bMessage.Id) : 0;

				this.GetCommunicationProtocol<Pop3>(pMailAccount.GetMailServiceHost())
					.Retrieve(pMailAccount.MailAddress.Value, pMailAccount.Password, mLastMessageId, pWindow)
					.ToList()
					.ForEach(bMessage =>
					{
						bMessage.From = null;
						pMailAccount.MailAddress.FromMessages.Add(bMessage);
					});

				MCDAL.Instance.Save();

				//se devuelven los nuevos mensajes
				return pMailAccount.MailAddress.FromMessages.Where(bMessage => bMessage.Id > mLastMessageId);

			}
			catch (Exception bException)
			{
				throw new FailOnRetrieve(Resources.Exceptions.MailService_Retrieve_FailOnRetrieve, bException);
			}
		}
		public void Send(MailAccount pMailAccount, MailMessage pMailMessage)
		{

			try
			{
				//guardar el mensaje en el repositorio
				pMailAccount.MailAddress.FromMessages.Add(pMailMessage);
				MCDAL.Instance.Save();

				this.GetCommunicationProtocol<Smtp>(pMailAccount.GetMailServiceHost()).SendMessage(pMailAccount.MailAddress.Value, pMailAccount.Password, pMailMessage);
			}
			catch (Exception bException)
			{
				throw new FailOnSend(Resources.Exceptions.MailService_Send_FailOnSend, bException);
			}

		}

		private T GetCommunicationProtocol<T>(string pMailServiceName) where T : Protocol
		{

			string mName = typeof(T).Name;

			MailService mMailService = MCDAL.Instance.MailServiceRepository.Single(MailServiceSelector.ByName(pMailServiceName));

			if (mMailService.Protocols.Any(bProtocol => bProtocol.Name == mName))
				return (T)mMailService.Protocols.Single(bProtocol => bProtocol.Name == mName);

			throw new UnknownComunicationProtocol(Resources.Exceptions.MailService_GetCommunicationProtocol_UnknownComunicationProtocol + mName);
		}
    }
}
