using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Accounting.Providers;

public class LedgerAccountStore (AppDbContext context, ILogger<LedgerAccountStore> logger)
{
	private readonly AppDbContext context = context;
	private readonly ILogger<LedgerAccountStore> logger = logger;

	public async Task<IList<CompanyLedgerAccount>> GetCompanyLedgerAccountsAsync (Company company, CancellationToken cancellationToken = default)
	{
		return await context.CompanyLedgerAccounts!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.Account)
			.ToListAsync(cancellationToken);
	}

	public async Task<IList<LedgerAccount>> GetSystemLedgerAccountsAsync (CancellationToken cancellationToken = default)
		=> await context.LedgerAccounts!.Where(p => p.System).ToListAsync(cancellationToken);

	public async Task<TransactionResult<LedgerAccount>> UpdateLedgerAccountAsync (LedgerAccount ledgerAccount)
	{
		try
		{
			var result = context.LedgerAccounts!.Update(ledgerAccount);
			await context.SaveChangesAsync();
			return TransactionResult<LedgerAccount>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<LedgerAccount>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateLedgerAccountAsync), logger));
		}
	}
}
