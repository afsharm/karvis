using System;

namespace SJ.Core
{
	public static class IoC
	{
		private static IDependencyResolver resolver;

		public static void RegisterResolver(IDependencyResolver dependencyResolver)
		{
			resolver = dependencyResolver;
		}

		public static T Resolve<T>()
		{
			return resolver.Resolve<T>();
		}

        public static object Resolve(string key)
        {
            return resolver.Resolve(key);
        }

		public static void RegisterImplementationOf(string id, Type implementation, LifeStyle lifeStyle)
		{
			resolver.RegisterImplementationOf(id, implementation, lifeStyle);
		}

		public static void RegisterImplementationOf(string id, Type service, Type implementation, LifeStyle lifeStyle)
		{
			resolver.RegisterImplementationOf(id, service, implementation, lifeStyle);
		}

		public static void RegisterImplementationOf(string id, Type service, Type implementation)
		{
			resolver.RegisterImplementationOf(id, service, implementation);
		}
	}
}