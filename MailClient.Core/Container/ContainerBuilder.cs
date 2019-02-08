using Microsoft.Practices.Unity.Configuration;
using System;
using Unity;

namespace MailClient.Core
{
	public class ContainerBuilder
	{
		private static ContainerBuilder iInstance;
		private static Lazy<IUnityContainer> iContainer = new Lazy<IUnityContainer>(() =>
		{
			return ContainerBuilder.RegisterTypes(new UnityContainer());
		});

		private ContainerBuilder()
		{
		}

		public static ContainerBuilder Instance
		{
			get
			{
				if (ContainerBuilder.iInstance == default(ContainerBuilder))
				{
					ContainerBuilder.iInstance = new ContainerBuilder();
				}
				return ContainerBuilder.iInstance;
			}
		}

		public T Resolve<T>() {
			if(!ContainerBuilder.iContainer.IsValueCreated) {
				throw new Exception();
			}
			return ContainerBuilder.iContainer.Value.Resolve<T>();
		}

		private static IUnityContainer RegisterTypes(IUnityContainer pContainer)
		{
			pContainer.LoadConfiguration();
			return pContainer;
		}
	}
}
