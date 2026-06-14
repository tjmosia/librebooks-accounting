using System.Security.Claims;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.IdentitySpace;

namespace Librebooks.Areas.Identity.Data;

public readonly struct LoginDto (User user, UserRole[] userRoles, Claim[] claims, Company? company)
{
	public readonly FindUserDto User = new(user);
	public readonly object[] Roles = [..Enumerable.Select(userRoles, p => new {
		p.Role?.Name,
		p.AssociatedTo
	})];

	public readonly object[] Claims = [..claims.Select(p => new {
		p.Type,
		p.Value
	})];
	public readonly object? Company = company == null ? null : new
	{
		Id = company.Id,
		Name = company.TradingName,
		Logo = company.Logo
	};
}
