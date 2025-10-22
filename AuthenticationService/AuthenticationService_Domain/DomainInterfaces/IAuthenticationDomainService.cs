using AuthenticationService_Domain.Models;

namespace AuthenticationService_Domain.DomainInterfaces
{
	public interface IAuthenticationDomainService
	{
		bool CanLogin(User user, string plainTextPassword, IPasswordVerifier verifier);
	}
}
