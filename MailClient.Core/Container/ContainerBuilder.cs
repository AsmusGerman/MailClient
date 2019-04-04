using Microsoft.Practices.Unity.Configuration;
using System;
using Unity;

namespace MailClient.Core
{
	public static class ContainerBuilder
	{
		private static Lazy<IUnityContainer> iContainer = new Lazy<IUnityContainer>(() =>
		{
			IUnityContainer mContainer = new UnityContainer();
			mContainer.LoadConfiguration();
			return mContainer;
		});

		public static T Resolve<T>()
		{
			return ContainerBuilder.iContainer.Value.Resolve<T>();
		}
	}
}
