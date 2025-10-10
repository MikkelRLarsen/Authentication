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

			// Creates our IoC Container
			var masterIoC = ServiceProvider_IoC.CreateServiceProvider(builder.Configuration);

			// Add all our Singleton servies to Blazor
			foreach (var singletonType in ServiceProvider_IoC.SingletonServices)
			{
				builder.Services.AddSingleton(singletonType, ioc => masterIoC.GetRequiredService(singletonType));
			}

			// Add all out Transient services to Blazor
			foreach (var transientType in ServiceProvider_IoC.TransientServices)
			{
				builder.Services.AddTransient(transientType, ioc => masterIoC.GetRequiredService(transientType));
			}

			// Add all out Scoped services to Blazor, with CreateScope so our Scoped Services have correct states on its depedencies
			foreach (var scopedType in ServiceProvider_IoC.ScopedServices)
			{
				builder.Services.AddScoped(scopedType, ioc =>
				{
					var scope = ServiceProvider_IoC.CreateScope();
					return scope.ServiceProvider.GetRequiredService(scopedType);
				});
			}

			var app = builder.Build();

            app.Run();
        }
    }
}
