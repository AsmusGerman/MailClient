using MailClient.BLL.Selectors;
using MailClient.Core;
using MailClient.DAL;
using MailClient.Shared;
using System;

namespace MailClient.BLL
{
    public class AuthenticationService : IAuthenticationService
    {
        private IEncryptor iEncryptor;
        private IRepository<MailAccount> iMailAccountRepository;
        
        public AuthenticationService()
        {
            this.iEncryptor = new DPEntryptor();
            this.iMailAccountRepository = MCDAL.Instance.GetRepository<MailAccount>();
        }

        /// <summary>
        /// obtiene la cuenta correspondiente al alias y a la contraseña
        /// </summary>
        public MailAccount LoginByAlias(string pAlias, string pPassword)
        {
            try
            {
                //Se obtiene la existencia de la cuenta que corresponde a los argumentos
                MailAccount mMailAccount = this.iMailAccountRepository.Single(MailAccountSelector.ByAlias(pAlias));
                if (this.iEncryptor.Decrypt(mMailAccount.Password) != pPassword)
                    throw new LoginException();

                if (mMailAccount.Deleted)
                    throw new LoginException();

                return mMailAccount;
            }
            catch (Exception bException)
            {
                throw new LoginException(Resources.Exceptions.LoginException, bException);
            }
        }

        /// <summary>
        /// obtiene la cuenta correspondiente al correo y a la contraseña
        /// </summary>
        public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
        {
            try
            {
                //Se obtiene la existencia de la cuenta que corresponde a los argumentos
                MailAccount mMailAccount = this.iMailAccountRepository.Single(MailAccountSelector.ByMailAddress(pMailAddress));
                if (this.iEncryptor.Decrypt(mMailAccount.Password) != pPassword)
                    throw new LoginException();

                if (mMailAccount.Deleted)
                    throw new LoginException();

                return mMailAccount;
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
                //se encripta la contraseña
                string mPassword = this.iEncryptor.Encrypt(pPassword);

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
                this.iMailAccountRepository.Create(mMailAccount);
                MCDAL.Instance.Save();
            }
            catch (Exception bException)
            {
                throw new RegisterException(Resources.Exceptions.RegisterException, bException);
            }

        }

        public void UpdateAccount()
        {
            MCDAL.Instance.Save();
        }
    }
}
