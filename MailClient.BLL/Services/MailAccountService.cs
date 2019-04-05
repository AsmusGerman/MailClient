using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Shared;
using MailClient.DAL;
using MailClient.Core;
using MailClient.BLL.Selectors;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MailClient.BLL.Configuration;

namespace MailClient.BLL
{
	public class MailAccountService : IMailAccountService
	{
		private MapperConfiguration MapperConfiguration { get; set; }
		private IDictionary<string, Protocol> iComunicationProtocols;

		public MailAccountService()
		{
			this.MapperConfiguration = new MapperConfiguration(bConfiguration =>
			{
				bConfiguration.AddProfile<BLLProfile>();
			});
		}

		public IEnumerable<MailMessage> Retrieve(MailAccount pMailAccount, int pWindow = 0)
		{
			try
			{
				int mLastMessageId = pMailAccount.MailAddress.FromMessages.Any() ? pMailAccount.MailAddress.FromMessages.Max(bMessage => bMessage.Id) : 0;

				IEnumerable<MailMessage> mMailMessages = this.GetCommunicationProtocol<Pop3>(pMailAccount.GetMailServiceHost())
					.Retrieve(pMailAccount.MailAddress.Value, pMailAccount.Password, mLastMessageId, pWindow)
					.AsQueryable()
					.ProjectTo<MailMessage>(this.MapperConfiguration)
					.ToList();

				//para los mensajes que tienen como remitente el correo del usuario
				// y que no existan en la bbdd
				IEnumerable<MailMessage> mFromListMessage =
					mMailMessages
						.Where(bMessage => bMessage.From.Value.Equals(pMailAccount.MailAddress.Value))
						.Where(bMessage => pMailAccount.MailAddress.ToMessages.Any(bSendedMessage => bSendedMessage.ExternalId != bMessage.ExternalId));

				//se agrega cada mensaje a la bbdd
				foreach (MailMessage bMailMessage in mFromListMessage)
				{
					pMailAccount.MailAddress.ToMessages.Add(bMailMessage);
				}

				//para los mensajes que tienen el correo del usuario en CC
				// y que no existan en la bbdd
				IEnumerable<MailMessage> mCCListMessage =
					mMailMessages
						.Where(bMessage => bMessage.Cc.Any(bCCMessage => bCCMessage.Value.Equals(pMailAccount.MailAddress.Value)))
						.Where(bMessage => pMailAccount.MailAddress.CcMessages.Any(bCCMessage => bCCMessage.ExternalId != bMessage.ExternalId));

				//se agrega cada mensaje a la bbdd
				foreach (MailMessage bMailMessage in mCCListMessage)
				{
					pMailAccount.MailAddress.ToMessages.Add(bMailMessage);
				}

				//para los mensajes que tienen el correo del usuario en BCC
				// y que no existan en la bbdd
				IEnumerable<MailMessage> mBCCListMessage =
					mMailMessages
						.Where(bMessage => bMessage.Bcc.Any(bBCCMessage => bBCCMessage.Value.Equals(pMailAccount.MailAddress.Value)))
						.Where(bMessage => pMailAccount.MailAddress.BccMessages.Any(bBCCMessage => bBCCMessage.ExternalId != bMessage.ExternalId));

				//se agrega cada mensaje a la bbdd
				foreach (MailMessage bMailMessage in mBCCListMessage)
				{
					pMailAccount.MailAddress.ToMessages.Add(bMailMessage);
				}

				//para los mensajes que no tienen como remitente el correo del usuario
				IEnumerable<MailMessage> mRecivedMailMessages = mMailMessages.Where(bMessage => !bMessage.From.Value.Equals(pMailAccount.MailAddress.Value));

				//se agrega cada mensaje a la bbdd
				foreach (MailMessage bMailMessage in mRecivedMailMessages)
				{
					pMailAccount.MailAddress.ToMessages.Add(bMailMessage);
				}

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
