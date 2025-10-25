using AuthenticationService_Shared.TestFolder.ReturnSingleton;
using AuthenticationServie_Shared.InversionOfControl.ExternalConfig;

namespace AuthenticationService_Shared.TestFolder.ReturnScoped
{
	public class TestImplementation : ITestInterface
	{
		private readonly string _randomStringAsANumber;
		private readonly ITestInterfaceSingleton _iTestInterfaceSingleton;
		private readonly IConfigurationDictonary _iConfigurationDictonary;
		private readonly IHttpClientFactory _httpClientFactory;

		public TestImplementation(ITestInterfaceSingleton iTestInterfaceSingleton, IConfigurationDictonary configurationDictonary, IHttpClientFactory httpClientFactory)
		{
			Random rnd = new();
			_randomStringAsANumber = rnd.Next(1, int.MaxValue).ToString();

			_iTestInterfaceSingleton = iTestInterfaceSingleton;
			_iConfigurationDictonary = configurationDictonary;
			_httpClientFactory = httpClientFactory;
		}

		public string ReturnARandomString()
		{
			string singletonString = _iTestInterfaceSingleton.ReturnAString();
			string configurationString = _iConfigurationDictonary.GetValue("RandomString");
			string httpClientAdressString = _httpClientFactory.CreateClient("AuthService").BaseAddress.ToString();

			return $"{_randomStringAsANumber} as a Scoped & {singletonString} as a Singleton & {configurationString} & {httpClientAdressString} from HttpClient";
		}
	}
}
