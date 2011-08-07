using System;

namespace SJ.Core
{
	public interface IDependencyResolver : IDisposable
	{
		T Resolve<T>();
		object Resolve(string key);
		void RegisterImplementationOf(string id, Type service, Type implementation, LifeStyle lifeStyle);

		void RegisterImplementationOf(string id, Type service, Type implementation);

		void RegisterImplementationOf(string id, Type implementation, LifeStyle lifeStyle);
	}
}
