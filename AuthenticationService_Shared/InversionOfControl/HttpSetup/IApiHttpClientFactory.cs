using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServie_Shared.InversionOfControl.HttpSetup
{
	internal interface IApiHttpClientFactory
	{
		HttpClient CreateClient(string name);
	}
}
