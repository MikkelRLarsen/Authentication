using AuthenticationSerivce_Domain.DomainInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationService_Infrastructure.Authentication
{
	public class Sha256PasswordVerifier : IPasswordVerifier
	{
		public bool Verify(string plainText, string hash)
		{
			using var sha = SHA256.Create();
			var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText));
			return Convert.ToHexString(bytes) == hash;
		}
	}
}
