using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServie_Shared.InversionOfControl.ExternalConfig
{
	public interface IConfigurationDictonary
	{
		string GetValue(string key);
	}
}
