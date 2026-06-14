using Microsoft.AspNetCore.Mvc;

namespace LibrebooksBlazor.Areas.Inventory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
	[HttpPost]
	[Route("create")]
	public IActionResult CreateItemAsync ()
	{

		return Ok();
	}
}
