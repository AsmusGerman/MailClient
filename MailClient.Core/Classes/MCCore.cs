using MailClient.Core.Selectors;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
    public class MCCore
    {
        private static MCCore iInstance;

        private IMailServiceCollection iMailServiceCollection;
        private IServicedMailAccountService iMailAccountService;
        private IAuthenticationService iAuthenticationService;


        private MailService iMailService;

        private MCCore()
        {
            this.LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            try
            {
                //carga de los servicios de correo
                this.iMailServiceCollection = Serializer.Instance.ReadFromFile<MailServiceCollection>(Resources.Files.Folder + Resources.Files.Services);

                //se resuelven los servicios de la aplicación
                this.iMailAccountService = ContainerBuilder.Resolve<IMailAccountService>();
                this.iAuthenticationService = ContainerBuilder.Resolve<IAuthenticationService>();

            }
            catch (Exception bException)
            {
                throw;
            }
        }

        public static MCCore Instance
        {
            get
            {
                if (MCCore.iInstance == default(MCCore))
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
                    throw new ArgumentNullException("los argumentos no pueden ser nulos");

                return this.iAuthenticationService.LoginByAlias(pAlias, pPassword);
            }
            catch (Exception bException)
            {
                throw;
            }
        }

        public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pMailAddress) || string.IsNullOrEmpty(pPassword))
                    throw new ArgumentNullException("los argumentos no pueden ser nulos");

                return this.iAuthenticationService.LoginByMailAddress(pMailAddress, pPassword);
            }
            catch (Exception bException)
            {

                throw;
            }
        }

        public void Register(string pAlias, string pMailAddress, string pPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pAlias) || string.IsNullOrEmpty(pMailAddress) || string.IsNullOrEmpty(pPassword))
                    throw new ArgumentNullException("los argumentos no pueden ser nulos");

                this.iAuthenticationService.Register(pAlias, pMailAddress, pPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MailMessage> UpdateInbox(MailAccount pMailAccount, int pWindow = 0)
        {
            try
            {
                string mMailServiceName = pMailAccount.GetMailServiceHost();
                MailService mMailService = this.iMailServiceCollection.ResolveByName(MailServiceSelector.ByName(mMailServiceName));
                return this.iMailAccountService.With(mMailService).Retrieve(pMailAccount, pWindow);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Send(MailAccount pMailAccount, MailMessage pMailMesage)
        {
            try
            {
                string mMailServiceName = pMailAccount.GetMailServiceHost();
                MailService mMailService = this.iMailServiceCollection.ResolveByName(MailServiceSelector.ByName(mMailServiceName));
                this.iMailAccountService.With(mMailService).Send(pMailAccount, pMailMesage);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
