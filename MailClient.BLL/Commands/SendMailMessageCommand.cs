using MailClient.BLL.Selectors;
using MailClient.Core;
using MailClient.DAL;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using MailClient.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL
{
    public class SendMailMessageCommand : AbstractServiceCommand
    {
        private IEncryptor iEncryptor;
        private IRepository<MailAccount> iMailAccountRepository;
        private IRepository<Shared.MailAddress> iMailAddressRepository;
        private IRepository<MailService> iMailServiceRepository;

        private MailAccount iMailAccount;
        private Shared.MailMessage iMailMessage;
        private IProtocol iProtocol;

        public SendMailMessageCommand()
        {
            this.iEncryptor = new DPEntryptor();
            this.iMailAccountRepository = MCDAL.Instance.GetRepository<MailAccount>();
            this.iMailAddressRepository = MCDAL.Instance.GetRepository<Shared.MailAddress>();
            this.iMailServiceRepository = MCDAL.Instance.GetRepository<MailService>();
        }

        public void Execute(int pMailAccountId, string pProtocolName, IEnumerable<string> pToMailAddresses, string pSubject, string pBody)
        {
            try
            {
                //se obtiene la cuenta del usuario activo
                this.iMailAccount = this.iMailAccountRepository.Single(MailAccountSelector.ById(pMailAccountId));

                //se crea el mensaje
                this.iMailMessage = new Shared.MailMessage()
                {
                    Subject = pSubject,
                    To = pToMailAddresses.Select(bMailAddress => new Shared.MailAddress()
                    {
                        Value = bMailAddress

                    }).ToList(),
                    Body = pBody
                };

                //se obtienen los datos del protocolo
                this.iProtocol = this.iMailServiceRepository
                    .Single(MailServiceSelector.ByName(this.iMailAccount.GetMailServiceHost()))
                    .Protocols.Single(bProtocol => bProtocol.Name.ToLower() == pProtocolName);

                iMailMessage.From = iMailAccount.MailAddress;
                //guardar el mensaje en el repositorio
                iMailAccount.MailAddress.FromMessages.Add(iMailMessage);
                iMailMessage.To = this.ResolveDbMailAddresses(iMailMessage.To).ToList();
                MCDAL.Instance.Save();

                this.Send(iMailAccount.MailAddress.Value, this.iEncryptor.Decrypt(iMailAccount.Password), iMailMessage);
            }
            catch (Exception bException)
            {
                throw new FailOnSend(Resources.Exceptions.MailService_Send_FailOnSend, bException);
            }
        }

        private void Send(string pMailAddress, string pPassword, Shared.MailMessage pMailMessage)
        {
            try
            {
                //se configura el cliente y se envía el correo
                new SmtpClient()
                {
                    Host = this.iProtocol.Host,
                    Port = this.iProtocol.Port,
                    EnableSsl = this.iProtocol.SSL,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(pMailAddress, pPassword)
                }.Send(pMailMessage.ToNetMailMessage());

            }
            catch (Exception bException)
            {
                throw new FailOnSend(Resources.Exceptions.Smtp_SendMessage_FailOnSend, bException);
            }
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
