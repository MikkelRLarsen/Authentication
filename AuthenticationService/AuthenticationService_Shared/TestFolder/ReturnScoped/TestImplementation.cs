using AuthenticationService_Shared.TestFolder.ReturnSingleton;
using AuthenticationServie_Shared.InversionOfControl.ExternalConfig;

namespace AuthenticationService_Shared.TestFolder.ReturnScoped
{
	public class TestImplementation : ITestInterface
	{
		private readonly string _randomStringAsANumber;
		private readonly ITestInterfaceSingleton _iTestInterfaceSingleton;
		private readonly IConfigurationDictonary _iConfigurationDictonary;
		public TestImplementation(ITestInterfaceSingleton iTestInterfaceSingleton, IConfigurationDictonary configurationDictonary)
		{
			Random rnd = new();
			_randomStringAsANumber = rnd.Next(1, int.MaxValue).ToString();

			_iTestInterfaceSingleton = iTestInterfaceSingleton;
			_iConfigurationDictonary = configurationDictonary;
		}

		public string ReturnARandomString()
		{
			string singletonString = _iTestInterfaceSingleton.ReturnAString();
			string configurationString = _iConfigurationDictonary.GetValue("RandomString");

			return $"{_randomStringAsANumber} as a Scoped & {singletonString} as a Singleton & {configurationString}";
		}
	}
}
