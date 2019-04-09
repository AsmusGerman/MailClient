using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Shared;
using MailClient.DAL;
using MailClient.BLL.Selectors;
using MailClient.Shared.Extensions;
using OpenPop.Pop3;

namespace MailClient.BLL
{
    public class MailAccountService : IMailAccountService
    {
        private IEncryptor iEncryptor;
        private IRepository<MailAccount> iMailAccountRepository;
        private IRepository<Shared.MailAddress> iMailAddressRepository;
        private IRepository<MailService> iMailServiceRepository;

        public MailAccountService()
        {
            this.iEncryptor = new DPEntryptor();
            this.iMailAccountRepository = MCDAL.Instance.GetRepository<MailAccount>();
            this.iMailAddressRepository = MCDAL.Instance.GetRepository<Shared.MailAddress>();
            this.iMailServiceRepository = MCDAL.Instance.GetRepository<MailService>();
        }

        public void Retrieve(int pMailAccountId, string pProtocolName, int pRetrieveWindowSize)
        {
            try
            {
                //se obtiene la cuenta del usuario activo
                MailAccount mMailAccount = this.iMailAccountRepository.Single(MailAccountSelector.ById(pMailAccountId));

                //se obtiene la cantidad de mensajes como base para obtener los siguientes
                int mLastMessageId =
                    mMailAccount.MailAddress.FromMessages.Any()
                    && mMailAccount.MailAddress.ToMessages.Any()
                        ? mMailAccount.MailAddress.FromMessages.Union(mMailAccount.MailAddress.ToMessages).Count()
                        : 0;

                //se obtienen los correos
                IEnumerable<MailMessage> mMailMessages =
                    this.Retrieve(pProtocolName,
                                    mMailAccount.GetMailServiceHost(),
                                    mMailAccount.MailAddress.Value,
                                    this.iEncryptor.Decrypt(mMailAccount.Password),
                                    mLastMessageId,
                                    pRetrieveWindowSize)
                        .Select(bMailMessage => bMailMessage.ToMailClientMailMessage())
                        .ToList();

                //para los mensajes que tienen como remitente el correo del usuario
                // y que no existan en la bbdd
                IEnumerable<MailMessage> mFromListMessage =
                    mMailMessages
                        .Where(bMessage => bMessage.From.Value.Equals(mMailAccount.MailAddress.Value))
                        .Where(bMessage => !mMailAccount.MailAddress.FromMessages.Any(bSendedMessage => bSendedMessage.ExternalId == bMessage.ExternalId)).ToList();

                //se agrega cada mensaje a la bbdd
                foreach (MailMessage bMailMessage in mFromListMessage)
                {
                    bMailMessage.From = mMailAccount.MailAddress;
                    bMailMessage.To = this.ResolveDbMailAddresses(bMailMessage.To).ToList();
                    mMailAccount.MailAddress.FromMessages.Add(bMailMessage);
                    MCDAL.Instance.Save();
                }

                //para los mensajes que no tienen como remitente el correo del usuario
                IEnumerable<MailMessage> mRecivedMailMessages =
                    mMailMessages
                        .Where(bMessage => !bMessage.From.Value.Equals(mMailAccount.MailAddress.Value))
                        .Where(bMessage => !mMailAccount.MailAddress.ToMessages.Any(bSendedMessage => bSendedMessage.ExternalId == bMessage.ExternalId)).ToList();


                //se agrega cada mensaje a la bbdd
                foreach (MailMessage bMailMessage in mRecivedMailMessages)
                {
                    bMailMessage.From = this.iMailAddressRepository.Single(MailAddressSelector.ByMailAddress(bMailMessage.From.Value)) ?? bMailMessage.From;
                    bMailMessage.To = this.ResolveDbMailAddresses(bMailMessage.To).ToList();
                    mMailAccount.MailAddress.ToMessages.Add(bMailMessage);
                    MCDAL.Instance.Save();
                }
            }
            catch
            {

            }
        }

        private IEnumerable<System.Net.Mail.MailMessage> Retrieve(string pProtocolName, string pMailServiceHost, string pMailAddress, string pPassword, int pOffset = 0, int pWindow = 0)
        {
            //se obtienen los datos del protocolo
            IProtocol mProtocol = this.iMailServiceRepository
                .Single(MailServiceSelector.ByName(pMailServiceHost))
                .Protocols.Single(bProtocol => bProtocol.Name.ToLower() == pProtocolName);

            return new Pop3(mProtocol).Retrieve(pMailAddress,
                                             pPassword,
                                             pOffset,
                                             pWindow);

        }


        public void Send(int pMailAccountId, string pProtocolName, IEnumerable<string> pToMailAddresses, string pSubject, string pBody)
        {
            try
            {
                //se obtiene la cuenta del usuario activo
                MailAccount mMailAccount = this.iMailAccountRepository.Single(MailAccountSelector.ById(pMailAccountId));

                //se crea el mensaje
                MailMessage mMailMessage = new Shared.MailMessage()
                {
                    Subject = pSubject,
                    To = pToMailAddresses.Select(bMailAddress => new Shared.MailAddress()
                    {
                        Value = bMailAddress

                    }).ToList(),
                    Body = pBody
                };

                mMailMessage.From = mMailAccount.MailAddress;
                //guardar el mensaje en el repositorio
                mMailAccount.MailAddress.FromMessages.Add(mMailMessage);
                mMailMessage.To = this.ResolveDbMailAddresses(mMailMessage.To).ToList();
                mMailMessage.DateSent = DateTime.Now.ToShortDateString();
                MCDAL.Instance.Save();

                this.Send(pProtocolName, mMailAccount.GetMailServiceHost(), mMailAccount.MailAddress.Value, this.iEncryptor.Decrypt(mMailAccount.Password), mMailMessage);
            }
            catch (Exception bException)
            {
                throw new FailOnSend(Resources.Exceptions.MailService_Send_FailOnSend, bException);
            }
        }

        private void Send(string pProtocolName, string pMailServiceHost, string pMailAddress, string pPassword, Shared.MailMessage pMailMessage)
        {
            //se obtienen los datos del protocolo
            IProtocol mProtocol = this.iMailServiceRepository
                .Single(MailServiceSelector.ByName(pMailServiceHost))
                .Protocols.Single(bProtocol => bProtocol.Name.ToLower() == pProtocolName);

            //se configura el cliente y se envía el correo
            new Smtp(mProtocol).Send(pMailAddress, pPassword, pMailMessage);
        }

        private IEnumerable<Shared.MailAddress> ResolveDbMailAddresses(IEnumerable<Shared.MailAddress> pSource)
        {
            IEnumerable<Shared.MailAddress> mDbMailAddresses =
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
