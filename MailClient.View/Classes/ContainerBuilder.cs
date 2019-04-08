using Microsoft.Practices.Unity.Configuration;
using System;
using Unity;
using Unity.Resolution;

namespace MailClient.View
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

        public static T Resolve<T>(string pName)
        {
            return ContainerBuilder.iContainer.Value.Resolve<T>(pName);
        }

        public static T Resolve<T>(string pName, params ResolverOverride[] pResolverOverrides)
        {
            return ContainerBuilder.iContainer.Value.Resolve<T>(pName, pResolverOverrides);
        }
    }
}
