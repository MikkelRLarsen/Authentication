using AuthenticationSerivce_Domain.Models;

namespace AuthenticationService_Application.InfrastructureInterfaces
{
	public interface IAuthService
	{
		public string GenerateToken(User user);
	}
}
