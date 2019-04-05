using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace MailClient.BLL
{
	public class Smtp : Protocol
	{
		#region public methods
		/// <summary>
		/// Envía un correo electrónico
		/// </summary>
		/// <param name="pMailAddress"></param>
		/// <param name="pPassword"></param>
		/// <param name="pMailMessage"></param>
		public void SendMessage(string pMailAddress, string pPassword, Shared.MailMessage pMailMessage)
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
					Host = this.Host,
					Port = this.Port,
					EnableSsl = this.SSL,
					UseDefaultCredentials = false,
					Credentials = mCredential
				}.Send(mMessage);

			}
			catch (Exception bException)
			{
				throw new FailOnSend(Resources.Exceptions.Smtp_SendMessage_FailOnSend, bException);
				throw new UnknownErrorException(Resources.Exceptions.Smtp_SendMessage_UnknownErrorException, bException);
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
			try
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
			catch (Exception bException)
			{
				throw new FailOnCreateMessage(Resources.Exceptions.Smtp_CreateMessage_FailOnCreateMessage, bException);
			}

		}
		#endregion
	}
}
