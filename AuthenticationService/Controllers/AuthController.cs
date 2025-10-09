using AuthenticationService_Application.Commands.Login;
using AuthenticationService_Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly LoginCommand _loginHandler;

		public AuthController(LoginCommand loginHandler)
		{
			_loginHandler = loginHandler;
		}

		// POST: api/auth/Login
		[HttpPost("login")]
		public async Task<IActionResult> Post([FromBody] LoginCommandDto command)
		{
			if (string.IsNullOrWhiteSpace(command.Username) || string.IsNullOrWhiteSpace(command.Password))
				return BadRequest("Username and Password are required.");

			var jwtToken = await _loginHandler.ValidateLoginInformation(command);

			if (jwtToken == null)
				return Unauthorized("Invalid username or password.");

			// Return JWT token
			return Ok(new { Token = jwtToken });
		}
	}
}
