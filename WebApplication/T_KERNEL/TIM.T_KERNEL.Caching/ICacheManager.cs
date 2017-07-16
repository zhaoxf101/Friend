using System;

namespace TIM.T_KERNEL.Caching
{
	internal interface ICacheManager
	{
		bool Add(string key, object value, TableDependency tableDependency);

		object Get(string key);

		void Remove(string key);
	}
}
