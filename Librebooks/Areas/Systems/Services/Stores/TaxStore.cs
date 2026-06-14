using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores;

public class TaxStore : DbStoreBase
{
	public TaxStore (AppDbContext context, ILogger<TaxStore>? logger)
		: base(context, logger) { }

	public async Task<TransactionResult<Tax>> CreateAsync (Tax vat, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.Taxes!.AddAsync(vat, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<Tax>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<Tax>.Failure(GeneralError);
		}
	}

	public async Task<TransactionResult<Tax>> UpdateAsync (Tax vat, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.Taxes!.Update(vat);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<Tax>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<Tax>.Failure(GeneralError);
		}
	}

	public async Task<Tax?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await context!.Taxes!.FindAsync([id], cancellationToken);

	public async Task<TransactionResult> DeleteAsync (Tax[] taxes, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.Taxes!.RemoveRange(taxes);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			IList<TransactionError> errors = [];
			if (IsForeignKeyViolation(ex))
				errors.Add(new(description: taxes.Length > 1 ? "One or more taxes are currently in use." : "Tax is currently in use."));
			if (errors.Any())
				errors.Add(GeneralError);
			return TransactionResult.Failure([.. errors]);
		}
	}

	public async Task<IList<Tax>> FindAllAsync (CancellationToken cancellationToken = default)
		=> await context!.Taxes!.Where(p => p.System).ToListAsync(cancellationToken);
}
