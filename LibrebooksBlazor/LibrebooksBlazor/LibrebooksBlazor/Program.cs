using LibrebooksBlazor.Areas.Companies.Services;
using LibrebooksBlazor.Components;
using LibrebooksBlazor.Data;
using LibrebooksBlazor.Models.Entity.IdentitySpace;
using LibrebooksBlazor.Providers.Services;
using LibrebooksBlazor.Providers.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("Schema")));

services.AddIdentity<User, Role>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = false;
})
	.AddDefaultTokenProviders()
	.AddEntityFrameworkStores<AppDbContext>();

services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/auth";
	options.LogoutPath = "/logout";
	options.ReturnUrlParameter = "returnUrl";
	options.AccessDeniedPath = "/";
	options.ExpireTimeSpan = TimeSpan.FromHours(12);
});

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

services.AddScoped<VerificationStore>();
services.AddScoped<IVerificationService, VerificationService>();
services.AddScoped<ICompanyStore, CompanyStore>();
services.AddScoped<ICompanyManager, CompanyManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/logout", async context =>
{
	await context.SignOutAsync(IdentityConstants.ApplicationScheme);
	context.Response.Redirect("/");
});
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(LibrebooksBlazor.Client._Imports).Assembly);

app.Run();

