using LibrebooksBlazor.Areas.Companies.Services;
using LibrebooksBlazor.Models.Entity.IdentitySpace;
using LibrebooksBlazor.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibrebooksBlazor.Areas.Identity.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController (UserManager<User> userManager, ICompanyStore companyStore, UserService userService) : ControllerBase
	{
		private readonly ICompanyStore CompanyStore = companyStore;
		private readonly UserManager<User> UserManager = userManager;
		private readonly UserService UserService = userService;

		[Authorize, HttpGet("confirm-login")]
		public async Task<IActionResult> ConfirmLogin ()
		{
			var user = await UserService.GetCurrentUserAsync();

			if (user == null)
				return Unauthorized();
			var company = await CompanyStore.FindByUserIdAsync(user.Id);
			return Ok(new
			{
				User = new
				{
					user.Email,
					user.FirstName,
					user.LastName,
					user.Photo
				}
			});
		}
	}
}
