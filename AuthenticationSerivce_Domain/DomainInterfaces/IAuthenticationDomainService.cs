using AuthenticationSerivce_Domain.Models;

namespace AuthenticationSerivce_Domain.DomainInterfaces
{
	public interface IAuthenticationDomainService
	{
		bool CanLogin(User user, string plainTextPassword, IPasswordVerifier verifier);
	}
}
