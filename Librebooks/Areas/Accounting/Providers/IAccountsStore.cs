using Librebooks.Core.Operations;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;

namespace Librebooks.Areas.Accounting.Providers;

public interface IAccountsStore
{
	/**********************************************************
	 * LEDGER ACCOUNTS
	 **********************************************************/
	Task<LedgerAccount?> FindByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<IList<LedgerAccount>> FindBySysAsync (CancellationToken cancellationToken = default);
	Task<TransactionResult<LedgerAccount>> UpdateAsync (LedgerAccount ledgerAccount, CancellationToken cancellationToken = default);
	Task<TransactionResult<LedgerAccount>> CreateAsync (LedgerAccount ledgerAccount, CancellationToken cancellationToken = default);
	Task<TransactionResult> DeleteAsync (params LedgerAccount[] accounts);

	/**********************************************************
	 * LEDGER ACCOUNTS
	 **********************************************************/
	Task<TransactionResult<LedgerAccountCategory>> CreateOrUpdateCategoryAsync (LedgerAccountCategory category, CancellationToken cancellationToken = default);
	Task<LedgerAccountCategory?> FindCategoryByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<IList<LedgerAccountCategory>> FindCategoriesAsync (CancellationToken cancellationToken = default);
	Task<TransactionResult> DeleteCategoryAsync (params LedgerAccountCategory[] categories);


	/**********************************************************
	 * CASHFLOW CLASS
	 **********************************************************/
	Task<IList<LedgerAccountCashFlowType>> FindCashFlowTypesAsync (CancellationToken cancellationToken = default);
	Task<LedgerAccountCashFlowType?> FindCashflowTypeByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<TransactionResult<LedgerAccountCashFlowType>> CreateorUpdateCashFlowTypeAsync (LedgerAccountCashFlowType cashFlowType, CancellationToken cancellationToken = default);

	/**********************************************************
	 * JOURNAL ENTRIES
	 **********************************************************/
	Task<IList<JournalEntry>> FindJournalEntriesAsync (Company company, CancellationToken cancellationToken = default);
	Task<JournalEntry?> FindJournalEntryByIdAsync (Company company, int id, CancellationToken cancellationToken = default);
	Task<TransactionResult<JournalEntry>> CreateJournalEntryAsync (JournalEntry entry);
	Task<TransactionResult> CreateJournalEntriesAsync (params JournalEntry[] entry);
	Task<TransactionResult<JournalEntry>> UpdateJournalEntryAsync (JournalEntry entry);
	Task<TransactionResult> DeleteJournalEntriesAsync (params JournalEntry[] entries);
}
