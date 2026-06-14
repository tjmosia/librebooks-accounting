using LibrebooksBlazor.Models.Entity.IdentitySpace;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibrebooksBlazor.Extensions.Mvc
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class SessionControllerBase (
		UserManager<User>? userManager = null,
		SignInManager<User>? signInManager = null,
		ILogger<SessionControllerBase>? logger = null)
				: ControllerBase
	{
		protected readonly UserManager<User>? userManager = userManager;
		protected readonly SignInManager<User>? signInManager = signInManager;
		protected readonly ILogger<SessionControllerBase>? logger = logger;
	}
}
