using MailClient.DAL;
using MailClient.Services;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using MailClient.StringEncryption;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MailClient.Core
{
	public static class UserAccountController
	{
	//	private static IMailAccountRepository iRepository;
	//	public static MailAccount New(string pAlias, string pMailAddress, string pPassword)
	//	{
	//		try
	//		{

	//			if (Repository.GetAll().Any(_ => _.Alias.Equals(pAlias)))
	//				throw new ExistingMailAccount(Resources.ExceptionMessages.UserAccountController_NewUser_ExistingMailAccount);

	//			if (Repository.GetAll().Any(_ => _.MailAddress.Equals(pMailAddress)))
	//				throw new ExistingMailAccount(Resources.ExceptionMessages.UserAccountController_NewUser_ExistingMailAccount);

	//			string mPassword = CompositionRoot.Resolve<EncryptorFactory>()
	//								.Create<DPEntryptor>()
	//								.Encrypt(pPassword);

	//			MailAccount mNewUserAccount = new MailAccount()
	//			{
	//				Alias = pAlias,
	//				MailAddress = new MailAddress { Address = pMailAddress },
	//				Password = mPassword,
	//			};

	//			int mId = Repository.Add(mNewUserAccount);
	//			return new UserAccount(Repository.Get(mId));
	//		}
	//		catch (WellKnownException bException)
	//		{
	//			throw new FailOnNewUser(Resources.ExceptionMessages.UserAccountController_NewUser_FailOnNewUser, bException);
	//		}
	//		catch (Exception bException)
	//		{
	//			throw new UnknownErrorException(Resources.ExceptionMessages.UserAccountController_UknownErrorException, bException);
	//		}
	//	}

	//	public static bool VerifyCredentials(string pMailAddressOrAddress, string pPassword)
	//	{
	//		try
	//		{
	//			return Repository.Exists();
	//		}
	//		catch (WellKnownException bException)
	//		{
	//			throw new FailOnNewUser(Resources.ExceptionMessages.UserAccountController_NewUser_FailOnNewUser, bException);
	//		}
	//		catch (Exception bException)
	//		{
	//			throw new UnknownErrorException(Resources.ExceptionMessages.UserAccountController_UknownErrorException, bException);
	//		}
	//	}

	//	public static void SaveChanges()
	//	{
	//		Repository.SaveChanges();
	//	}

	//	public static UserAccount Get(string pMailAddressOrAlias, string pPassword)
	//	{

	//		try
	//		{
	//			IEncryptor mEncryptor = (IEncryptor) CompositionRoot.Resolve(typeof(DPEntryptor));
	//			Func<string, string> mDecryption = 
	//				mStringToEncrypt => mEncryptor.Decrypt(mStringToEncrypt);

	//			MailAccount mUser = default(MailAccount);
	//			if (IsMailAddress(pMailAddressOrAlias))
	//				mUser = Repository.GetByMailAddress(pMailAddressOrAlias, pPassword, mDecryption);
	//			else
	//				mUser = Repository.GetByAlias(pMailAddressOrAlias, pPassword, mDecryption);

	//			if (mUser == default(MailAccount))
	//				throw new UnknownMailAccount(Resources.ExceptionMessages.UserAccountController_GetUser_UknownMailAccount);

	//			return new UserAccount(mUser);
	//		}
	//		catch (WellKnownException bException)
	//		{
	//			throw new FailOnGetUser(Resources.ExceptionMessages.UserAccountController_GetUser_FailOnGetUser, bException);
	//		}
	//		catch (Exception bException)
	//		{
	//			throw new UnknownErrorException(Resources.ExceptionMessages.UserAccountController_UknownErrorException, bException);
	//		}
	//	}

	//	private static IMailAccountRepository Repository
	//	{
	//		get
	//		{
	//			if(iRepository == null)
	//				iRepository = CompositionRoot.Resolve<IUnitOfWork>().MailAccountRepository;
				
	//			return iRepository;
	//		}
	//	}

	//	private static bool IsMailAddress(string pMailAddress)
	//	{
	//		return
	//			new System.Net.Mail.MailAddress(pMailAddress)
	//				.Address.Equals(pMailAddress);
	//	}

	}
}
