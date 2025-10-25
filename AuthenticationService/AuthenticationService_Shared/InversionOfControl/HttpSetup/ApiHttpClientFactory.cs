using System.Net.Http;

namespace AuthenticationServie_Shared.InversionOfControl.HttpSetup
{
	public class ApiHttpClientFactory : IApiHttpClientFactory
	{
		private readonly IHttpClientFactory _factory;

		// Unused IHttpClientFactory wrapper since the IoC Container only is ussable in dotnet framework, so the IoC Container would have to be recoupled from Microsoft.Extensions.DepedencyInjection to be useful

		public ApiHttpClientFactory(IHttpClientFactory factory)
		{
			_factory = factory;
		}

		public HttpClient CreateClient(string name)
		{
			return _factory.CreateClient(name);
		}
	}
}
