using MailClient.BLL.Selectors;
using MailClient.DAL;
using MailClient.Shared;
using MailClient.Shared.Extensions;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.BLL
{
    public class RetrieveMailMessagesCommand : AbstractServiceCommand
    {
        private IEncryptor iEncryptor;
        private IRepository<MailAccount> iMailAccountRepository;
        private IRepository<Shared.MailAddress> iMailAddressRepository;
        private IRepository<MailService> iMailServiceRepository;

        private MailAccount iMailAccount;
        private int iRetrieveWindowSize;
        private IProtocol iProtocol;

        public RetrieveMailMessagesCommand()
        {
            this.iEncryptor = new DPEntryptor();
            this.iMailAccountRepository = MCDAL.Instance.GetRepository<MailAccount>();
            this.iMailAddressRepository = MCDAL.Instance.GetRepository<Shared.MailAddress>();
            this.iMailServiceRepository = MCDAL.Instance.GetRepository<MailService>();
        }

        public void Execute(int pMailAccountId, string pProtocolName, int pRetrieveWindowSize)
        {
            try
            {
                //se obtiene la cuenta del usuario activo
                this.iMailAccount = this.iMailAccountRepository.Single(MailAccountSelector.ById(pMailAccountId));
                this.iRetrieveWindowSize = pRetrieveWindowSize;

                //se obtienen los datos del protocolo
                this.iProtocol = this.iMailServiceRepository
                    .Single(MailServiceSelector.ByName(this.iMailAccount.GetMailServiceHost()))
                    .Protocols.Single(bProtocol => bProtocol.Name.ToLower() == pProtocolName);

                //se obtiene la cantidad de mensajes como base para obtener los siguientes
                int mLastMessageId =
                    this.iMailAccount.MailAddress.FromMessages.Any()
                    && this.iMailAccount.MailAddress.ToMessages.Any()
                        ? this.iMailAccount.MailAddress.FromMessages.Union(this.iMailAccount.MailAddress.ToMessages).Count()
                        : 0;

                //se obtienen los correos
                IEnumerable<MailMessage> mMailMessages =
                    this.Retrieve(this.iMailAccount.GetMailServiceHost(),
                                  this.iMailAccount.MailAddress.Value,
                                    this.iEncryptor.Decrypt(this.iMailAccount.Password),
                                    mLastMessageId,
                                    this.iRetrieveWindowSize)
                        .Select(bMailMessage => bMailMessage.ToMailClientMailMessage())
                        .ToList();

                //para los mensajes que tienen como remitente el correo del usuario
                // y que no existan en la bbdd
                IEnumerable<MailMessage> mFromListMessage =
                    mMailMessages
                        .Where(bMessage => bMessage.From.Value.Equals(this.iMailAccount.MailAddress.Value))
                        .Where(bMessage => !this.iMailAccount.MailAddress.FromMessages.Any(bSendedMessage => bSendedMessage.ExternalId == bMessage.ExternalId));

                //se agrega cada mensaje a la bbdd
                foreach (MailMessage bMailMessage in mFromListMessage)
                {
                    bMailMessage.From = this.iMailAccount.MailAddress;
                    bMailMessage.To = this.ResolveDbMailAddresses(bMailMessage.To).ToList();
                    this.iMailAccount.MailAddress.FromMessages.Add(bMailMessage);
                    MCDAL.Instance.Save();
                }

                //para los mensajes que no tienen como remitente el correo del usuario
                IEnumerable<MailMessage> mRecivedMailMessages =
                    mMailMessages
                        .Where(bMessage => !bMessage.From.Value.Equals(this.iMailAccount.MailAddress.Value))
                        .Where(bMessage => !this.iMailAccount.MailAddress.ToMessages.Any(bSendedMessage => bSendedMessage.ExternalId == bMessage.ExternalId));


                //se agrega cada mensaje a la bbdd
                foreach (MailMessage bMailMessage in mRecivedMailMessages)
                {
                    bMailMessage.From = this.iMailAddressRepository.Single(MailAddressSelector.ByMailAddress(bMailMessage.From.Value)) ?? bMailMessage.From;
                    bMailMessage.To = this.ResolveDbMailAddresses(bMailMessage.To).ToList();
                    this.iMailAccount.MailAddress.ToMessages.Add(bMailMessage);
                    MCDAL.Instance.Save();
                }
            }
            catch
            {

            }
        }

        private IEnumerable<System.Net.Mail.MailMessage> Retrieve(string pMailServiceHost,
                                                                  string pMailAddress,
                                                                  string pPassword,
                                                                  int pOffset = 0,
                                                                  int pWindow = 0)
        {
            try
            {
                if (pWindow == 0)
                    return new List<System.Net.Mail.MailMessage>();

                //se inicia la conexión pop3
                Pop3Client mPop3Client = new Pop3Client();
                mPop3Client.Connect(this.iProtocol.Host, this.iProtocol.Port, this.iProtocol.SSL);
                mPop3Client.Authenticate(pMailAddress, pPassword);

                //se actualiza el tamaño de la ventana
                pWindow = pWindow > mPop3Client.GetMessageCount() ? mPop3Client.GetMessageCount() - 1 : pWindow;

                //se obtienen los mensajes
                return mPop3Client.GetMessageCount() > 0 ?
                    Enumerable.Range(pOffset + 1, pWindow)
                        .Select(bIndex => this.GetNetMailMessage(mPop3Client.GetMessage(bIndex)))
                        .ToList()
                    : new List<System.Net.Mail.MailMessage>();
            }
            catch (Exception bException)
            {
                throw new FailOnRetrieve(Resources.Exceptions.Pop3_Retrieve_UnknownErrorException, bException);
            }
        }

        private System.Net.Mail.MailMessage GetNetMailMessage(Message pPopMessage)
        {
            System.Net.Mail.MailMessage mMailMessage = pPopMessage.ToMailMessage();
            mMailMessage.Headers.Add("ExternalId", pPopMessage.Headers.MessageId);
            return mMailMessage;
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
