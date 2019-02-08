﻿using MailClient.Core;
using MailClient.DAL;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using MailClient.Shared.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL
{
	public class AuthServiceBLL : IAuthServiceBLL
	{
		public MailAccount LoginByAlias(string pAlias, string pPassword)
		{
			try
			{
				MailAccount bMailAccount = MCDAL.Instance.MailAccountRepository.Single(MailAccountSelector.ByAlias(pAlias));

				string mPassword = new DPEntryptor().Decrypt(pPassword);

				if (bMailAccount.Password != mPassword) {
					throw new Exception();
				}

				return bMailAccount;
			}
			catch (Exception bException)
			{
				throw new LoginException("", bException);
			}
		}

		public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
		{

			try
			{
				MailAccount bMailAccount = MCDAL.Instance.MailAccountRepository.Single(MailAccountSelector.ByMailAddress(pMailAddress));
				
				string mPassword = new DPEntryptor().Decrypt(pPassword);

				if (bMailAccount.Password != mPassword)
				{
					throw new Exception();
				}

				return bMailAccount;
			}
			catch (Exception bException)
			{
				throw new LoginException("", bException);
			}
		}

		public void Register(string pAlias, string pMailAddress, string pPassword)
		{
			string mPassword = new DPEntryptor().Encrypt(pPassword);


			MailAddress mMailAddress = new MailAddress
			{
				Value = pMailAddress
			};


			MailAccount mMailAccount = new MailAccount
			{
				Alias = pAlias,
				MailAddress = mMailAddress,
				Password = mPassword
			};


			MCDAL.Instance.MailAccountRepository.Create(mMailAccount);
		}
	}
}
