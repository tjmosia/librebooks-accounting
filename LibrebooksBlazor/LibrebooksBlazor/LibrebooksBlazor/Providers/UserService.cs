using System.Security.Claims;
using LibrebooksBlazor.Data;
using LibrebooksBlazor.Models.Entity.IdentitySpace;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Providers;

public class UserService (AuthenticationStateProvider provider, UserManager<User> userManager, IDbContextFactory<AppDbContext> factory, SignInManager<User> signInManager)
{
	private readonly AuthenticationStateProvider stateProvider = provider;
	private readonly UserManager<User> userManager = userManager;
	private readonly SignInManager<User> signInManager = signInManager;

	public async Task<User?> GetCurrentUserAsync ()
	{
		if (await IsSignedInAsync())
		{
			var claimsPrincipal = await GetClaimsPrincipalAsync();
			using AppDbContext db = await factory.CreateDbContextAsync();

			return await db.Users.Where(p => p.NormalizedEmail == claimsPrincipal.Identity!.Name)
					.FirstOrDefaultAsync();

		}

		return null;
	}

	public async Task<bool> IsSignedInAsync ()
	{
		var claimsPrincipal = await GetClaimsPrincipalAsync();
		if (claimsPrincipal == null || claimsPrincipal.Identity == null)
			return false;
		return claimsPrincipal.Identity.IsAuthenticated;
	}

	public async Task SignOutAsync ()
	{
		await signInManager.SignOutAsync();
	}

	public async Task<ClaimsPrincipal> GetClaimsPrincipalAsync ()
		=> (await stateProvider.GetAuthenticationStateAsync()).User;

}
