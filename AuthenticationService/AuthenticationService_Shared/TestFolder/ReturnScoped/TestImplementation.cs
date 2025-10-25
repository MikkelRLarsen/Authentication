using AuthenticationService_Shared.TestFolder.ReturnSingleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Shared.TestFolder.ReturnScoped
{
	public class TestImplementation : ITestInterface
	{
		private readonly string RandomStringAsANumber;
		private readonly ITestInterfaceSingleton singleton;
		public TestImplementation(ITestInterfaceSingleton depedency)
		{
			Random rnd = new();
			RandomStringAsANumber = rnd.Next(1, int.MaxValue).ToString();

			singleton = depedency;
		}

		public string ReturnARandomString()
		{
			string singletonString = singleton.ReturnAString();
			return $"{RandomStringAsANumber} as a Scoped & {singletonString} as a Singleton";
		}
	}
}
