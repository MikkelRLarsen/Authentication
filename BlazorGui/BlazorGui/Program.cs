using BlazorGui.Client.Pages;
using BlazorGui.Components;
using BlazorGui.Shared;
using Shared.InversionOfControl;

namespace BlazorGui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
