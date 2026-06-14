using LibrebooksBlazor.Core.Operations;
using LibrebooksBlazor.Models.Entity.BankingSpace;
using LibrebooksBlazor.Models.Entity.CompanySpace;
using LibrebooksBlazor.Models.Entity.SalesSpace;

namespace LibrebooksBlazor.Areas.Companies.Services;

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
			logger!.LogError("***DB Error occurred with Exception while trying to remove Sales Person:*** \n\n{message}", ex.Message);
			return TransactionResult.Failure();
		}
	}

	public async Task<TransactionResult> DeleteAsync (Company company)
	{

		try
		{
			context.Companies!.Remove(company);
			await context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while removing Company:*** \n\n{message}", ex.Message);
			return TransactionResult.Failure();
		}
		return TransactionResult.Success;
	}

	public async Task<TransactionResult> DeleteTaxTypeAsync (CompanyTax companyTaxType)
	{
		ArgumentNullException.ThrowIfNull(companyTaxType.TaxType, nameof(companyTaxType.TaxType));

		if (companyTaxType.TaxType!.System)
			return TransactionResult.Failure(Error.Create("", "Cannot remove a system tax type."));

		try
		{
			context.CompanyTaxes!.Remove(companyTaxType);
			context.Taxes!.Remove(companyTaxType.TaxType);
			await context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to remove Sales Person:*** \n\n{message}", ex.Message);
			return TransactionResult.Failure();
		}

		return TransactionResult.Success;
	}

	public async Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount)
	{
		try
		{
			context.BankAccounts!.Remove(bankAccount);
			await context.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception)
		{
			return TransactionResult.Failure();
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
		catch (Exception)
		{
			return TransactionResult.Failure();
		}
	}
}
