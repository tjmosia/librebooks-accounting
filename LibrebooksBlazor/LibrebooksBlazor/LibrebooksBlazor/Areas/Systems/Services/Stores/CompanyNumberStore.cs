using LibrebooksBlazor.Core.EFCore;
using LibrebooksBlazor.Core.Operations;
using LibrebooksBlazor.Data;
using LibrebooksBlazor.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Areas.Systems.Services.Stores;

public class CompanyNumberStore (AppDbContext context, ILogger<CompanyNumberStore> logger)
	: DbStoreBase(context, logger)
{
	public async Task<CompanySetup?> FindCurrentAsync (CancellationToken cancellationToken = default)
		=> await context!.CompanySetup!.FirstOrDefaultAsync(cancellationToken);

	public async Task<TransactionResult<CompanySetup>> CreateAsync (CompanySetup setup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.CompanySetup!.AddAsync(setup, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<CompanySetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<CompanySetup>.Failure(GeneralError);
		}
	}

	public async Task<TransactionResult<CompanySetup>> UpdateAsync (CompanySetup setup, CancellationToken cancellationToken = default)
	{
		try
		{
			setup.RefreshConcurrencyToken();
			var result = context!.CompanySetup!.Update(setup);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<CompanySetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<CompanySetup>.Failure(GeneralError);
		}
	}
}
