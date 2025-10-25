using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;
using AuthenticationServie_Shared;
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

            builder.Services.AddControllers();

            // Register our Services to our IoC-Container
            ServiceProvider_IoC.RegisterServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            app.Run();
        }
    }
}
