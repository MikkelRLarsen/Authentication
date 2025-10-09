using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Application.DTOs
{
	public record LoginCommandDto(string Username, string Password);
}
