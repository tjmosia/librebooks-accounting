using System.Security.Claims;
using LibrebooksBlazor.Data;
using LibrebooksBlazor.Models.Entity.IdentitySpace;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Providers.Services;

public class UserService (IDbContextFactory<AppDbContext> factory)
{
	private readonly IDbContextFactory<AppDbContext> factory = factory;
	public async Task<User?> GetUserAsync (ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken = default)
	{
		using var db = await factory.CreateDbContextAsync(cancellationToken);

		return await db.Users.Where(p => p.UserName == claimsPrincipal.Identity!.Name!.ToLower())
				.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IList<Claim>> GetClaimsAsync (User user, CancellationToken cancellationToken = default)
	{
		using var db = await factory.CreateDbContextAsync(cancellationToken);

		return await db.UserClaims.Where(p => p.UserId == user.Id)
			.Select(p => new Claim(p.ClaimType!, p.ClaimValue!))
			.ToListAsync(cancellationToken);
	}

	public async Task<IList<Claim>> GetRolesAsync (User user, CancellationToken cancellationToken = default)
	{
		using var db = await factory.CreateDbContextAsync(cancellationToken);

		return await db.UserClaims.Where(p => p.UserId == user.Id)
			.Select(p => new Claim(p.ClaimType!, p.ClaimValue!))
			.ToListAsync(cancellationToken);
	}

	public bool IsSignedIn (AuthenticationState state)
	{
		return state.User != null
			&& state.User.Identity != null
			&& state.User.Identity.IsAuthenticated;
	}
}
