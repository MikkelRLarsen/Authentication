using Microsoft.Extensions.DependencyInjection;
using Shared.InversionOfControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGui.Shared
{
	public class ScopedServiceProviderBridge<T> : IDisposable
		where T : class
	{
		private readonly IServiceScope _scope;
		public T Service { get; }

		public ScopedServiceProviderBridge()
		{
			_scope = ServiceProvider_IoC.CreateScope();
			Service = _scope.ServiceProvider.GetRequiredService<T>();
		}

		public void Dispose()
		{
			_scope.Dispose();
		}
	}
}
