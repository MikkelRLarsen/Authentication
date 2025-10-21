using AuthenticationSerivce_Domain.DomainInterfaces;
using AuthenticationSerivce_Domain.Models;
using AuthenticationService_Application.DTOs;
using AuthenticationService_Application.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Application.Commands.Login
{
	public class LoginCommand : ILoginCommand
	{
		private readonly IUserRepository _userRepo;
		private readonly IPasswordVerifier _verifier;
		private readonly IAuthenticationDomainService _authenticationDomainService;
		private readonly IAuthService _authService;

		public LoginCommand(IUserRepository userRepo, IPasswordVerifier verifier, IAuthenticationDomainService authenticationDomainService, IAuthService authService)
		{
			_userRepo = userRepo;
			_verifier = verifier;
			_authenticationDomainService = authenticationDomainService;
			_authService = authService;
		}

		public async Task<string?> ValidateLoginInformation(LoginCommandDto command)
		{
			// Gets the user from Database
			var userDto = await _userRepo.GetUserByUsernameAsync(command.Username);

			if (userDto == null) return null;

			// Creates the user from Dto
			var user = new User(userDto.Username, userDto.PasswordHash, userDto.Role);

			// Checks if the password matches with the hashedpassword from database
			if (_authenticationDomainService.CanLogin(user, command.Password, _verifier) == false) return null;

			return _authService.GenerateToken(user);
		}
	}
}
