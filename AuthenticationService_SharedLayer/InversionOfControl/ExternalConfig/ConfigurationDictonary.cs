using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServie_SharedLayer.InversionOfControl.ExternalConfig
{
	public class ConfigurationDictonary : IConfigurationDictonary
	{
		private readonly Dictionary<string, string> _configurations = new Dictionary<string, string>();

		public ConfigurationDictonary(IConfiguration configuration)
		{
			// Add configurations
			_configurations.Add("JwtSecret", configuration["JwtSecret"]!);
			_configurations.Add("DefaultConnection", configuration["DefaultConnection"]!);
		}

		public string GetValue(string key)
		{
			if (!_configurations.TryGetValue(key, out var value))
				throw new KeyNotFoundException($"Configuration key '{key}' not found.");

			return value;
		}
	}
}
