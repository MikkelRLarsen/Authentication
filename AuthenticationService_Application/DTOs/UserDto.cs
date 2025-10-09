using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Application.DTOs
{
	public class UserDto
	{
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string Role { get; set; } = null!;
	}
}
