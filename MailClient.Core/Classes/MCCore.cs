using MailClient.Core.Selectors;
using MailClient.Shared;
using System;
using System.Collections.Generic;

namespace MailClient.Core
{
    public class MCCore
    {
        private static MCCore iInstance;

        private IMailAccountService iMailAccountService;
        private IAuthenticationService iAuthenticationService;


        private MailService iMailService;

        private MCCore()
        {
			try
			{
				//se resuelven los servicios de la aplicación
				this.iMailAccountService = ContainerBuilder.Resolve<IMailAccountService>();
				this.iAuthenticationService = ContainerBuilder.Resolve<IAuthenticationService>();
			}
			catch (Exception bException)
			{
				throw new MCCoreInstantiateException(Resources.ExceptionMessages.MCCoreIntantiateException, bException);
			}
		}

        public static MCCore Instance
        {
            get
            {
                if (MCCore.iInstance == null)
                {
                    MCCore.iInstance = new MCCore();
                }
                return MCCore.iInstance;
            }
        }

        public MailAccount LoginByAlias(string pAlias, string pPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pAlias) || string.IsNullOrEmpty(pPassword))
                    throw new ArgumentNullException(Resources.ExceptionMessages.LoginNullOrEmptyArgumentException);

                return this.iAuthenticationService.LoginByAlias(pAlias, pPassword);
            }
            catch (Exception bException)
            {
                throw new LoginException(Resources.ExceptionMessages.LoginException, bException);
            }
        }

        public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pMailAddress) || string.IsNullOrEmpty(pPassword))
					throw new ArgumentNullException(Resources.ExceptionMessages.LoginNullOrEmptyArgumentException);

				return this.iAuthenticationService.LoginByMailAddress(pMailAddress, pPassword);
            }
            catch (Exception bException)
            {
				throw new LoginException(Resources.ExceptionMessages.LoginException, bException);

			}
		}

        public void Register(string pAlias, string pMailAddress, string pPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pAlias) || string.IsNullOrEmpty(pMailAddress) || string.IsNullOrEmpty(pPassword))
					throw new ArgumentNullException(Resources.ExceptionMessages.LoginNullOrEmptyArgumentException);

				this.iAuthenticationService.Register(pAlias, pMailAddress, pPassword);
            }
            catch (Exception bException)
            {
				throw new RegisterException(Resources.ExceptionMessages.RegisterException, bException);
			}
		}

        public IEnumerable<MailMessage> UpdateInbox(MailAccount pMailAccount, int pWindow = 0)
        {
            try
            {
                return this.iMailAccountService.Retrieve(pMailAccount, pWindow);
            }
            catch (Exception bException)
            {
				throw new UpdateInboxException(Resources.ExceptionMessages.UpdateInboxException, bException);
			}
        }

        public void Send(MailAccount pMailAccount, MailMessage pMailMesage)
        {
            try
            {
                this.iMailAccountService.Send(pMailAccount, pMailMesage);
            }
			catch (Exception bException)
			{
				throw new SendException(Resources.ExceptionMessages.SendException, bException);
			}

		}
    }
}
