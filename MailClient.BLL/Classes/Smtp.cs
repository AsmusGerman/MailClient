using MailClient.Shared;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace MailClient.BLL
{
    public class Smtp {

        IProtocol iProtocol;
        public Smtp(IProtocol pProtocol)
        {
            this.iProtocol = pProtocol;
        }
        #region public methods
        /// <summary>
        /// Envía un correo electrónico
        /// </summary>
        /// <param name="pMailAddress"></param>
        /// <param name="pPassword"></param>
        /// <param name="pMailMessage"></param>
        public void Send(string pMailAddress, string pPassword, Shared.MailMessage pMailMessage)
        {
            try
            {
                //se castea el mensaje
                System.Net.Mail.MailMessage mMessage = this.CreateMessage(pMailMessage);

                //se crean las credenciales para enviar
                NetworkCredential mCredential = new NetworkCredential(pMailAddress, pPassword);

                //se configura el cliente y se envía el correo
                new SmtpClient()
                {
                    Host = this.iProtocol.Host,
                    Port = this.iProtocol.Port,
                    EnableSsl = this.iProtocol.SSL,
                    UseDefaultCredentials = true,
                    Credentials = mCredential
                }.Send(mMessage);

            }
            catch (Exception bException)
            {
                throw new FailOnSend("Error en SMTP, posible problema con la conexión, autenticación o durante el envío del correo. Vea excepción interna.", bException);
            }

        }
        #endregion

        #region private methods
        /// <summary>
        /// castea el mensaje al tipo MailMessage de .Net
        /// </summary>
        /// <param name="pMailMessage"></param>
        /// <returns></returns>
        private System.Net.Mail.MailMessage CreateMessage(Shared.MailMessage pMailMessage)
        {
            System.Net.Mail.MailMessage mMailMessage = new System.Net.Mail.MailMessage()
            {
                From = new System.Net.Mail.MailAddress(pMailMessage.From.Value),
                Subject = pMailMessage.Subject,
                Body = pMailMessage.Body,
            };

            pMailMessage.To.ToList()
                .ForEach(bAddress => mMailMessage.To.Add(bAddress.Value));

            return mMailMessage;
        }
        #endregion
    }
}
