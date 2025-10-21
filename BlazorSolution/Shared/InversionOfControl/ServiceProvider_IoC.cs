using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Shared.InversionOfControl.HttpSetup;


namespace Shared.InversionOfControl
{
	public class ServiceProvider_IoC
	{
		private static ServiceProvider? _serviceProvider;

		public static List<Type> ScopedServices { get; } = new();
		public static List<Type> SingletonServices { get; } = new();
		public static List<Type> TransientServices { get; } = new();

		public static ServiceProvider CreateServiceProvider(IConfiguration configuration)
		{
			if (_serviceProvider != null) return _serviceProvider;

			ServiceCollection services = new ServiceCollection();

			// Configuration from IConfig needed for Services are definered here

			// HttpClient injection into ServiceCollection
			HttpClientModule.RegisterHttpClients(services, configuration);

			// Singleton Services added here

			// Scoped Services added here
			/* Example
			 * RegisterService<IEmployeeService, EmployeeService>(services, ServiceLifetimeType.Scoped);		 
			 */

			// Transient Services added here

			// DbContext added here

			_serviceProvider = services.BuildServiceProvider();

			return _serviceProvider;
		}

		private static void RegisterService<TService, TImplementation>(IServiceCollection services,ServiceLifetimeType lifetime)
			where TService : class
			where TImplementation : class, TService
		{
			switch (lifetime)
			{
				case ServiceLifetimeType.Singleton:
					services.AddSingleton<TService, TImplementation>();
					SingletonServices.Add(typeof(TService));
					break;

				case ServiceLifetimeType.Scoped:
					services.AddScoped<TService, TImplementation>();
					ScopedServices.Add(typeof(TService));
					break;

				case ServiceLifetimeType.Transient:
					services.AddTransient<TService, TImplementation>();
					TransientServices.Add(typeof(TService));
					break;

				default:
					throw new Exception($"{nameof(TService)} with {nameof(TImplementation)} was given {lifetime} which is an invalid lifetime");
			}
		}

		public static ServiceProvider GetServiceProvider() => _serviceProvider ?? throw new Exception("Service provider was not created. Run CreateServiceProviderFirst!");
		public static IServiceScope CreateScope() => GetServiceProvider().CreateScope();
	}	

	internal enum ServiceLifetimeType
	{
		Singleton,
		Scoped,
		Transient
	}
}
