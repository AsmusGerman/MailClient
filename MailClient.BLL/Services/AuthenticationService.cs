using MailClient.BLL.Selectors;
using MailClient.Core;
using MailClient.DAL;
using MailClient.Shared;
using System;
using System.Linq;

namespace MailClient.BLL
{
    public class AuthenticationService : IAuthenticationService
    {
        private IEncryptor Encryptor;

        public AuthenticationService(IEncryptor pEncriptor)
        {
            this.Encryptor = pEncriptor;
        }

        /// <summary>
        /// obtiene la cuenta correspondiente al alias y a la contraseña
        /// </summary>
		public bool LoginByAlias(string pAlias, string pPassword)
        {
            try
            {
                //se obtiene el repositorio de las cuentas de correo
                IRepository mRespository = MCDAL.Instance.MailAccountRepository;
                //Se obtiene la existencia de la cuenta que corresponde a los argumentos
                return mRespository.Query(MailAccountSelector.ByAlias(pAlias, pPassword));
            }
            catch (Exception bException)
            {
                throw new LoginException(Resources.Exceptions.LoginException, bException);
            }
        }

        /// <summary>
        /// obtiene la cuenta correspondiente al correo y a la contraseña
        /// </summary>
        public bool LoginByMailAddress(string pMailAddress, string pPassword)
        {

            try
            {
                //se obtiene el repositorio de las cuentas de correo
                IRepository mRespository = MCDAL.Instance.MailAccountRepository;
                //Se obtiene la existencia de la cuenta que corresponde a los argumentos
                return mRespository.Query(MailAccountSelector.ByMailAddress(pMailAddress, pPassword));
            }
            catch (Exception bException)
            {
                throw new LoginException(Resources.Exceptions.LoginException, bException);
            }
        }

        /// <summary>
        /// registra una nueva cuenta de correo
        /// </summary>
        public void Register(string pAlias, string pMailAddress, string pPassword)
        {
            try
            {
                //se obtiene el repositorio de las cuentas de correo
                IRepository mRespository = MCDAL.Instance.MailAccountRepository;

                //se encripta la contraseña
                string mPassword = this.Encryptor.Encrypt(pPassword);

                //se crea la direccion de correo
                MailAddress mMailAddress = new MailAddress
                {
                    Value = pMailAddress
                };

                //se crea la cuenta de correo
                MailAccount mMailAccount = new MailAccount
                {
                    Alias = pAlias,
                    MailAddress = mMailAddress,
                    Password = mPassword
                };

                //se agrega la nueva cuenta al repositorio
                mRespository.Create(mMailAccount);
            }
            catch (Exception bException)
            {
                throw new RegisterException(Resources.Exceptions.RegisterException, bException);
            }

        }
    }
}
