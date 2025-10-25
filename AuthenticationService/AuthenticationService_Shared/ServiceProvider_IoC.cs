using AuthenticationService_Shared.TestFolder.ReturnScoped;
using AuthenticationService_Shared.TestFolder.ReturnSingleton;
using AuthenticationServie_Shared.InversionOfControl.ExternalConfig;
using AuthenticationServie_Shared.InversionOfControl.HttpSetup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServie_Shared
{
	public class ServiceProvider_IoC
	{
		public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			// ConfigurationDictonary are injected into ServiceCollection
			services.AddSingleton<IConfigurationDictonary, ConfigurationDictonary>(sp => new ConfigurationDictonary(configuration));

			// HttpClient injection into ServiceCollection
			HttpClientModule.RegisterHttpClients(services, configuration);

			// Singleton Services added here
			RegisterService<ITestInterfaceSingleton, TestImplementationSingleton>(services, ServiceLifetimeType.Singleton);

			// Scoped Services added here
			// RegisterService<IEmployeeService, EmployeeService>(services, ServiceLifetimeType.Scoped);	
			RegisterService<ITestInterface, TestImplementation>(services, ServiceLifetimeType.Scoped);	 

			// Transient Services added here

			// DbContext added here
		}

		private static void RegisterService<TService, TImplementation>(IServiceCollection services, ServiceLifetimeType lifetime)
			where TService : class
			where TImplementation : class, TService
		{
			switch (lifetime)
			{
				case ServiceLifetimeType.Singleton:
					services.AddSingleton<TService, TImplementation>();
					break;

				case ServiceLifetimeType.Scoped:
					services.AddScoped<TService, TImplementation>();
					break;

				case ServiceLifetimeType.Transient:
					services.AddTransient<TService, TImplementation>();
					break;

				default:
					throw new Exception($"{nameof(TService)} with {nameof(TImplementation)} was given {lifetime} which is an invalid lifetime");
			}
		}
	}

	internal enum ServiceLifetimeType
	{
		Singleton,
		Scoped,
		Transient
	}
}
