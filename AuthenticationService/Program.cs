using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using MySql.Data.MySqlClient;

namespace AuthenticationService
{
    // DTO til JSON binding
    record UserLogin(string Username, string Password);

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRateLimiter(options =>
            {
                options.AddPolicy("LoginPolicy", context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetTokenBucketLimiter(ip, key => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 2,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(60),
                        TokensPerPeriod = 2,
                        AutoReplenishment = true
                    });
                });

                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.PermitLimit = 2; // Amount of attemps
                    opt.Window = TimeSpan.FromSeconds(10); // Time before PermitLimit is reached

                    // 2 tries per 10 seconds

                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 2;
                });
            });

            var app = builder.Build();

            app.UseRateLimiter();

            string connString = builder.Configuration.GetConnectionString("Default");

            string HashPassword(string password)
            {
                using var sha256 = SHA256.Create();
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }

            app.MapPost("/register", async (UserLogin input) =>
            {
                using var conn = new MySqlConnection(connString);
                await conn.OpenAsync();

                var hash = HashPassword(input.Password);

                var cmd = new MySqlCommand("INSERT INTO Users (Username, PasswordHash) VALUES (@u, @p)", conn);
                cmd.Parameters.AddWithValue("@u", input.Username);
                cmd.Parameters.AddWithValue("@p", hash);

                await cmd.ExecuteNonQueryAsync();
                return Results.Ok("User registered!");
            });

            app.MapPost("/login", async (UserLogin input) =>
            {
                using var conn = new MySqlConnection(connString);
                await conn.OpenAsync();

                var cmd = new MySqlCommand("SELECT PasswordHash FROM Users WHERE Username=@u", conn);
                cmd.Parameters.AddWithValue("@u", input.Username);

                var result = await cmd.ExecuteScalarAsync();
                if (result == null) return Results.Unauthorized();

                var storedHash = result.ToString();
                var loginHash = HashPassword(input.Password);

                return storedHash == loginHash ? Results.Ok("Login successful!") : Results.Unauthorized();
            })
                .RequireRateLimiting("fixed");

            app.Run();
        }
    }
}
