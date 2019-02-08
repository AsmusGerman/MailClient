using MailClient.DAL;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using Unity;
using Unity.Injection;


namespace MailClient.BLL
{
	public static class CompositionRoot
	{
		private static IUnityContainer iContainer;

		/// <summary>
		/// Devuelve la intancia del contenedor
		/// </summary>
		private static IUnityContainer Container
		{
			get
			{
				//si no está instanciado, se crea un nuevo contenedor
				if (iContainer == default(IUnityContainer))
					iContainer = Configure();

				return iContainer;
			}
			set
			{
				iContainer = value;
			}
		}

		/// <summary>
		/// Resuelve la instancia del tipo <typeparamref name="T"/>
		/// </summary>
		public static T Resolve<T>(string pName = "")
		{
			if (string.IsNullOrEmpty(pName))
				return Container.Resolve<T>();

			return Container.Resolve<T>(pName);
		}

		public static object Resolve(Type pType)
		{
			return Container.Resolve(pType);
		}

		private static IUnityContainer Configure()
		{
			return new UnityContainer()
				.RegisterEncryptorComponents()
				.RegisterCommunicationProtocolComponents()
				.RegisterMailServiceComponents()
				.RegisterUnitOfWork();
		}

		#region private methods
		private static IUnityContainer RegisterCommunicationProtocolComponents(this IUnityContainer pContainer)
		{
			return pContainer
				.RegisterType<ICommunicationProtocol, Pop3>(nameof(Pop3))
				.RegisterType<ICommunicationProtocol, Smtp>(nameof(Smtp))
				.RegisterType<ICommunicationProtocol, NullComunicationProtocol>(string.Empty);
		}

		private static IUnityContainer RegisterMailServiceComponents(this IUnityContainer pContainer)
		{
			IEnumerable<ICommunicationProtocol> mProtocols = new List<ICommunicationProtocol>() {
							pContainer.Resolve<Pop3>(nameof(Pop3)),
							pContainer.Resolve<Smtp>(nameof(Smtp))
						};

			return pContainer
				.RegisterType<IMailService, GmailMailService>("gmail", new InjectionConstructor(mProtocols))
				.RegisterType<IMailService, YahooMailService>("yahoo", new InjectionConstructor(mProtocols))
				.RegisterType<IMailService, NullMailService>(string.Empty);
		}

		private static IUnityContainer RegisterEncryptorComponents(this IUnityContainer pContainer)
		{
			return pContainer
				.RegisterType<IEncryptor, DPEntryptor>(nameof(DPEntryptor))
				.RegisterType<IEncryptor, NullEncryptor>(string.Empty);
		}

		private static IUnityContainer RegisterUnitOfWork(this IUnityContainer pContainer)
		{
			return pContainer
				.RegisterSingleton<IUnitOfWork, UnitOfWork>();
		}

		#endregion
	}
}
