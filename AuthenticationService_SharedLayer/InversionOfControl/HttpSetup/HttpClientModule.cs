using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AuthenticationServie_SharedLayer.InversionOfControl.HttpSetup
{
	public static class HttpClientModule
	{
		internal static void RegisterHttpClients(IServiceCollection services, IConfiguration config)
		{
			// Defined in appsetting or with Enviroment settings
			var baseUrl = config["Api:BaseUrl"] ?? throw new Exception("Api:BaseUrl not configuered");

			// Microsoft’s factory
			services.AddHttpClient();

			// Abstract factory our services can depend on
			services.AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>();

			// Registrer named client
			services.AddHttpClient("AuthService", client => client.BaseAddress = new Uri($"{baseUrl}/auth/"));
		}
	}

}
