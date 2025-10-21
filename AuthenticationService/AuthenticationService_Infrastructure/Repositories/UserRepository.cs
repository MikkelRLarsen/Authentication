using AuthenticationService_Application.DTOs;
using AuthenticationService_Application.InfrastructureInterfaces;
using AuthenticationServie_Shared.InversionOfControl.ExternalConfig;
using MySql.Data.MySqlClient;

namespace AuthenticationService_Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly string _connectionString;

		public UserRepository(IConfigurationDictonary configurationDictonary)
		{
			_connectionString = configurationDictonary.GetValue("DefaultConnection");
		}

		public async Task<UserDto?> GetUserByUsernameAsync(string username)
		{
			using var conn = new MySqlConnection(_connectionString);
			await conn.OpenAsync();

			var cmd = new MySqlCommand("SELECT Username, PasswordHash, Role FROM Users WHERE Username=@u", conn);
			cmd.Parameters.AddWithValue("@u", username);

			using var reader = await cmd.ExecuteReaderAsync();
			if (!reader.Read()) return null;

			return new UserDto
			{
				Username = (string)reader["Username"],
				PasswordHash = (string)reader["PasswordHash"],
				Role = (string)reader["Role"]
			};
		}
	}
}
