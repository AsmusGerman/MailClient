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

		//TODO: SERVICES

		internal IMailServiceBLL iMailServiceBLL;
		internal IAuthServiceBLL iAuthServiceBLL;

		private MCCore()
		{
			string bConfigurationDirectory = Directory.GetCurrentDirectory() + Resources.Files.Folder;
			this.iMailServiceBLL = ContainerBuilder.Instance.Resolve<IMailServiceBLL>();
			this.iAuthServiceBLL = ContainerBuilder.Instance.Resolve<IAuthServiceBLL>();
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

				return this.iAuthServiceBLL.LoginByAlias(pAlias, pPassword);
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

				return this.iAuthServiceBLL.LoginByMailAddress(pMailAddress, pPassword);
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

				this.iAuthServiceBLL.Register(pAlias, pMailAddress, pPassword);
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
				return this.iMailServiceBLL.Retrieve(pMailAccount, pWindow);
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
				this.iMailServiceBLL.Send(pMailAccount, pMailMesage);
			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
