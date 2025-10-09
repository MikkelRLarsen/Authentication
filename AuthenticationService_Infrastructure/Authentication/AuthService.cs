using AuthenticationSerivce_Domain.Models;
using AuthenticationService_Application.InfrastructureInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService_Infrastructure.Authentication
{
	public class AuthService : IAuthService
	{
		private readonly string _jwtSecret;

		public AuthService(IConfigurationDictonary config)
		{
			_jwtSecret = config.GetValue("JwtSecret");
		}

		public string GenerateToken(User user)
		{
			var key = Encoding.UTF8.GetBytes(_jwtSecret);

			var claims = new[]
			{
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Role, user.Role)
		    };

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
