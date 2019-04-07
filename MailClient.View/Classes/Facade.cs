using MailClient.Core;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace MailClient.View
{
	public class Facade
	{
		private static Facade iInstance;
        public Notifier Notifier { get; private set; }
		private Facade()
		{
           this.Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }


        /// <summary>
        /// Obtiene la única instancia de la fachada
        /// </summary>
        public static Facade Instance
		{
			get
			{
				if (Facade.iInstance == null)
				{
					Facade.iInstance = new Facade();
				}
				return Facade.iInstance;
			}
		}

		/// <summary>
		/// Llama al core para ingresar a la aplicación mediante el alias de la cuenta
		/// </summary>
		public async Task<MailAccount> LoginByAlias(string pAlias, string pPassword)
		{
			try
			{
				return await Task.Run(() => MCCore.Instance.LoginByAlias(pAlias, pPassword));
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.LoginException, bException);
			}

		}

		/// <summary>
		/// Llama al core para ingresar a la aplicación mediante el correo electrónico de la cuenta
		/// </summary>
		public async Task<MailAccount> LoginByMailAddress(string pMailAddress, string pPassword)
		{
			try
			{
				return await Task.Run(() => MCCore.Instance.LoginByMailAddress(pMailAddress, pPassword));
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.LoginException, bException);
			}
		}

		/// <summary>
		/// Pide al core que registre una cuenta con los valores parametrizados
		/// </summary>
		public async Task Register(string pAlias, string pMail, string pPassword)
		{
			try
			{
				await Task.Run(() => MCCore.Instance.Register(pAlias, pMail, pPassword));
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.RegisterException, bException);
			}
		}

		/// <summary>
		/// Pide al core que actualice la casilla de correos de la cuenta teniendo en cuenta la cantidad de mensajes que debe descargar
		/// </summary>
		public async Task UpdateInbox(MailAccount pUserAccount, int pWindow = 0)
		{
			try
			{
				await Task.Run(() => MCCore.Instance.UpdateInbox(pUserAccount, pWindow));
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.UpdateInboxException, bException);
			}
		}

		/// <summary>
		/// Pide al core que envíe un correo
		/// </summary>
		public async Task Send(MailAccount pUserAccount, MailMessage pMailMessage)
		{
			try
			{
				await Task.Run(() => MCCore.Instance.Send(pUserAccount, pMailMessage));
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.SendException, bException);
			}
		}

        public async Task UpdateAccount()
        {
            try
            {
                await Task.Run(() => MCCore.Instance.UpdateAccount());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
