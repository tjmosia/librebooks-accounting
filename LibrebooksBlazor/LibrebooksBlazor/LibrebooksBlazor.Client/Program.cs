using LibrebooksBlazor.Client.Providers.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(options => new HttpClient
{
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddSingleton<IIdentityService, IdentityService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

await builder.Build().RunAsync();
