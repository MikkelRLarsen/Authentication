using AuthenticationSerivce_Domain.DomainInterfaces;
using AuthenticationSerivce_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService_Application.DomainServices
{
	public class AuthenticationDomainService : IAuthenticationDomainService
	{
		private readonly IPasswordVerifier _verifier;

		public AuthenticationDomainService(IPasswordVerifier verifier)
		{
			_verifier = verifier;
		}

		public bool CanLogin(User user, string plainTextPassword, IPasswordVerifier verifier)
		{
			if (user == null) throw new Exception("User was null while trying to Login");

			// Get and chekcs if the password matches with Password Hash from database
			bool isPasswordCorrect = _verifier.Verify(plainTextPassword, user.PasswordHash);

			return isPasswordCorrect;
		}
	}
}
