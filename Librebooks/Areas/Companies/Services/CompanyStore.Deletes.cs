using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SalesSpace;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	public async Task<TransactionResult> DeleteSalesPersonAsync (SalesPerson salesPerson)
	{
		try
		{
			context.SalesPeople!.Remove(salesPerson);
			context.Contacts!.Remove(salesPerson.Contact!);
			await context!.SaveChangesAsync();

			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteSalesPersonAsync), logger));
		}
	}

	public async Task<TransactionResult> DeleteAsync (Company company)
	{

		try
		{
			context.Companies!.Remove(company);
			await context.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteAsync), logger));
		}
	}

	public async Task<TransactionResult> DeleteTaxTypeAsync (CompanyTax companyTaxType)
	{
		try
		{
			context.CompanyTaxes!.Remove(companyTaxType);
			await context.SaveChangesAsync();

			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteTaxTypeAsync), logger));
		}
	}

	public async Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount)
	{
		try
		{
			context.BankAccounts!.Remove(bankAccount);
			await context.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteBankAccountAsync), logger));
		}
	}

	public async Task<TransactionResult> DeleteLogoAsync (CompanyLogo companyLogo)
	{
		try
		{
			context.CompanyLogos!.Remove(companyLogo);
			await context.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteLogoAsync), logger));
		}
	}
}
