using AuthenticationService_Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Application.Commands.Login
{
	public interface ILoginCommand
	{
		Task<string?> ValidateLoginInformation(LoginCommandDto command);
	}
}
