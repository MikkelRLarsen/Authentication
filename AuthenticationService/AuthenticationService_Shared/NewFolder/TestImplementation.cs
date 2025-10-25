using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Shared.NewFolder
{
	public class TestImplementation : ITestInterface
	{
		private readonly string RandomStringAsANumber;
		public TestImplementation()
		{
			Random rnd = new();
			RandomStringAsANumber = rnd.Next(1, int.MaxValue).ToString();	
		}

		public string ReturnARandomString()
		{
			return RandomStringAsANumber;
		}
	}
}
