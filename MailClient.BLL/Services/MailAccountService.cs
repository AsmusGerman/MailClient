using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Shared;
using MailClient.DAL;
using MailClient.BLL.Selectors;
using MailClient.Shared.Extensions;
using MailClient.Core;

namespace MailClient.BLL
{
    public class MailAccountService : IMailAccountService
    {
        private IEncryptor iEncryptor;
        private IRepository<MailAddress> iMailAddressRepository;
        private IRepository<MailService> iMailServiceRepository;

        public MailAccountService()
        {
            this.iEncryptor = new DPEntryptor();
            this.iMailAddressRepository = MCDAL.Instance.GetRepository<MailAddress>();
            this.iMailServiceRepository = MCDAL.Instance.GetRepository<MailService>();
        }

        public void Retrieve(MailAccount pMailAccount, int pWindow = 0)
        {
            try
            {
                //se obtiene el útlimo id para obtener los siguientes
                int mLastMessageId = 
                    pMailAccount.MailAddress.FromMessages.Any() 
                    && pMailAccount.MailAddress.ToMessages.Any() 
                        ? pMailAccount.MailAddress.FromMessages.Union(pMailAccount.MailAddress.ToMessages).Count() 
                        : 0;

                //se obtienen los correos
                IEnumerable<MailMessage> mMailMessages = 
                    this.GetCommunicationProtocol<Pop3>(pMailAccount.GetMailServiceHost())
                        .Retrieve(pMailAccount.MailAddress.Value, 
                                    this.iEncryptor.Decrypt(pMailAccount.Password),
                                    mLastMessageId,
                                    pWindow)
                        .Select(bMailMessage => bMailMessage.ToMailClientMailMessage())
                        .ToList();

                //para los mensajes que tienen como remitente el correo del usuario
                // y que no existan en la bbdd
                IEnumerable<MailMessage> mFromListMessage =
                    mMailMessages
                        .Where(bMessage => bMessage.From.Value.Equals(pMailAccount.MailAddress.Value))
                        .Where(bMessage => !pMailAccount.MailAddress.FromMessages.Any(bSendedMessage => bSendedMessage.ExternalId == bMessage.ExternalId));

                //se agrega cada mensaje a la bbdd
                foreach (MailMessage bMailMessage in mFromListMessage)
                {
                    bMailMessage.From = pMailAccount.MailAddress;
                    bMailMessage.To = this.ResolveDbMailAddresses(bMailMessage.To).ToList();
                    pMailAccount.MailAddress.FromMessages.Add(bMailMessage);
                    MCDAL.Instance.Save();
                }

                //para los mensajes que no tienen como remitente el correo del usuario
                IEnumerable<MailMessage> mRecivedMailMessages =
                    mMailMessages
                        .Where(bMessage => !bMessage.From.Value.Equals(pMailAccount.MailAddress.Value))
                        .Where(bMessage => !pMailAccount.MailAddress.ToMessages.Any(bSendedMessage => bSendedMessage.ExternalId == bMessage.ExternalId));


                //se agrega cada mensaje a la bbdd
                foreach (MailMessage bMailMessage in mRecivedMailMessages)
                {
                    bMailMessage.From = this.iMailAddressRepository.Single(MailAddressSelector.ByMailAddress(bMailMessage.From.Value)) ?? bMailMessage.From;
                    bMailMessage.To = this.ResolveDbMailAddresses(bMailMessage.To).ToList();
                    pMailAccount.MailAddress.ToMessages.Add(bMailMessage);
                    MCDAL.Instance.Save();
                }

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
                pMailMessage.From = pMailAccount.MailAddress;
                //guardar el mensaje en el repositorio
                pMailAccount.MailAddress.FromMessages.Add(pMailMessage);
                pMailMessage.To = this.ResolveDbMailAddresses(pMailMessage.To).ToList();
                MCDAL.Instance.Save();

                this.GetCommunicationProtocol<Smtp>(pMailAccount.GetMailServiceHost())
                    .SendMessage(pMailAccount.MailAddress.Value, this.iEncryptor.Decrypt(pMailAccount.Password), pMailMessage);
            }
            catch (Exception bException)
            {
                throw new FailOnSend(Resources.Exceptions.MailService_Send_FailOnSend, bException);
            }

        }

        private T GetCommunicationProtocol<T>(string pMailServiceName) where T : IProtocol, new()
        {

            string mName = typeof(T).Name.ToLower();

            MailService mMailService = this.iMailServiceRepository.Single(MailServiceSelector.ByName(pMailServiceName));

            if (mMailService.Protocols.Any(bProtocol => bProtocol.Name.ToLower() == mName))
            {
                IProtocol mProtocol = mMailService.Protocols.Single(bProtocol => bProtocol.Name.ToLower() == mName);
                return new T()
                {
                    Host = mProtocol.Host,
                    Name = mProtocol.Name,
                    Port = mProtocol.Port,
                    SSL = mProtocol.SSL
                };
            }

            throw new UnknownComunicationProtocol(Resources.Exceptions.MailService_GetCommunicationProtocol_UnknownComunicationProtocol + mName);
        }

        private IEnumerable<MailAddress> ResolveDbMailAddresses(IEnumerable<MailAddress> pSource)
        {
            IEnumerable<MailAddress> mDbMailAddresses =
                pSource.Select(bMailAddress =>
                                this.iMailAddressRepository.Single(MailAddressSelector.ByMailAddress(bMailAddress.Value)))
                        .Where(bMailAddress => bMailAddress != null)
                        .ToList();

            return pSource
                .Where(bMailAddress => !mDbMailAddresses.Any(bexistingMailAddress => bexistingMailAddress.Value == bMailAddress.Value))
                .Union(mDbMailAddresses);
        }
    }
}
