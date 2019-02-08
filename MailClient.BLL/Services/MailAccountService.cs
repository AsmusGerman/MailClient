using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Shared;
using MailClient.DAL;
using MailClient.Core;

namespace MailClient.BLL
{
	public class MailAccountService : IMailAccountService
	{
		private MailService iMailService;
		private IDictionary<string, Protocol> iComunicationProtocols;

		public MailService MailService
		{
			get
			{
				return this.iMailService;
			}
			private set
			{
				this.iMailService = value;

				this.iComunicationProtocols = new Dictionary<string, Protocol>();
				foreach (Protocol bProtocol in this.iMailService.Protocols)
				{
					this.iComunicationProtocols.Add(bProtocol.Name, bProtocol);
				}
			}
		}

		public MailAccountService() { }


		public IEnumerable<MailMessage> Retrieve(MailAccount pMailAccount, int pWindow = 0)
		{
			try
			{
				if (this.iMailService == default(MailService))
					throw new Exception();


				int mLastMessageId = pMailAccount.MailAddress.FromMessages.Any() ? pMailAccount.MailAddress.FromMessages.Max(bMessage => bMessage.Id) : 0;

				this.GetCommunicationProtocol<Pop3>()
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
				if (this.iMailService == default(MailService))
					throw new Exception();

				//guardar el mensaje en el repositorio
				pMailAccount.MailAddress.FromMessages.Add(pMailMessage);
				MCDAL.Instance.Save();

				this.GetCommunicationProtocol<Smtp>().SendMessage(pMailAccount.MailAddress.Value, pMailAccount.Password, pMailMessage);
			}
			catch (Exception bException)
			{
				throw new FailOnSend(Resources.Exceptions.MailService_Send_FailOnSend, bException);
			}

		}

		private T GetCommunicationProtocol<T>() where T : Protocol
		{

			string mName = typeof(T).Name;

			if (this.iComunicationProtocols.ContainsKey(mName))
				return (T)this.iComunicationProtocols[mName];

			throw new UnknownComunicationProtocol(Resources.Exceptions.MailService_GetCommunicationProtocol_UnknownComunicationProtocol + mName);
		}

        public IMailAccountService With(MailService pMailService)
        {
            this.iMailService = pMailService;
            return this;
        }
    }
}
