using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServie_Shared.InversionOfControl.HttpSetup
{
	public class ApiHttpClientFactory : IApiHttpClientFactory
	{
		private readonly IHttpClientFactory _factory;

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
