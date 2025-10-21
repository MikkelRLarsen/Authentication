namespace AuthenticationSerivce_Domain.DomainInterfaces
{
	public interface IPasswordVerifier
	{
		bool Verify(string plainText, string hash);
	}
}
