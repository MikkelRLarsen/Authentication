using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Map YARP
app.MapReverseProxy();

app.Urls.Clear();
app.Urls.Add("http://+:8080");

app.Run();