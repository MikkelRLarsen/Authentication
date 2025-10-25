using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Shared.TestFolder.ReturnSingleton
{
	internal class TestImplementationSingleton : ITestInterfaceSingleton
	{
		private readonly string RandomStringAsANumber;
		public TestImplementationSingleton()
		{
			Random rnd = new();
			RandomStringAsANumber = rnd.Next(1, int.MaxValue).ToString();
		}

		public string ReturnAString()
		{
			return RandomStringAsANumber;
		}
	}
}
