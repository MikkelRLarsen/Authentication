using AuthenticationService_Application.Commands.Login;
using AuthenticationService_Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService_Shared.NewFolder;

namespace AuthenticationService_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ILoginCommand _loginCommand;
		private readonly ITestInterface _test;

		public AuthController(ITestInterface testImplementation)
		{
			//_loginCommand = loginCommand;
			_test = testImplementation;
		}

		// POST: api/auth/Login
		[HttpPost("login")]
		public async Task<IActionResult> Post([FromBody] LoginCommandDto command)
		{
			if (string.IsNullOrWhiteSpace(command.Username) || string.IsNullOrWhiteSpace(command.Password))
				return BadRequest("Username and Password are required.");

			var jwtToken = await _loginCommand.ValidateLoginInformation(command);

			if (jwtToken == null)
				return Unauthorized("Invalid username or password.");

			// Return JWT token
			return Ok(new { Token = jwtToken });
		}

		[HttpGet("test")]
		public async Task<string> TestMethod()
		{
			return _test.ReturnARandomString();
		}

	}
}
