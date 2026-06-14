using LibrebooksBlazor.Areas.Systems.Data;
using LibrebooksBlazor.Areas.Systems.Models;
using LibrebooksBlazor.Areas.Systems.Services;
using LibrebooksBlazor.Core.Operations;
using Microsoft.AspNetCore.Mvc;

namespace LibrebooksBlazor.Areas.Systems.Controllers;

[ApiController]
public class DateFormatsController (ISystemsManager systemManager)
	: SystemsControllerBase(systemManager)
{
	[HttpGet]
	public async Task<IList<DateFormatData>> OnGetAsync ()
	{
		return [.. (await Manager.GetDateFormatsAsync()).Select(p => new DateFormatData(p))];
	}

	[HttpPost]
	public async Task<IActionResult> OnPostAsync ([FromBody] DateFormatsAddModels.Request model)
	{
		var modelState = DateFormatsAddModels.Validate(model);

		if (!modelState.IsValid)
			return BadRequest(TransactionResult.Failure([..modelState.Errors
				.Select( p=> TransactionError.Create(p.PropertyName, p.ErrorMessage))]));

		var result = Manager.AddDateFormatAsync(new()
		{
			Format = model.Format,
		});

		return Ok(result);
	}

	[HttpPost("{id}")]
	public async Task<IActionResult> OnPatchAsync (int id, [FromBody] DateFormatsAddModels.Request model, CancellationToken cancellationToken)
	{
		var modelState = DateFormatsAddModels.Validate(model);

		if (!modelState.IsValid)
			return BadRequest(TransactionResult.Failure([..modelState.Errors
				.Select( p=> TransactionError.Create(p.PropertyName, p.ErrorMessage))]));

		var format = Manager.FindDateFormatByIdAsync(id, cancellationToken);

		var result = Manager.AddDateFormatAsync(new()
		{
			Format = model.Format,
		});

		return Ok(result);
	}
}
