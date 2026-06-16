using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SupplierSpace;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	public async Task<TransactionResult<Company>> UpdateAsync (Company company)
	{
		company.RefreshConcurrencyToken();

		try
		{
			var result = context.Companies!.Update(company);
			await context.SaveChangesAsync();
			return TransactionResult<Company>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<Company>.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateAsync), logger));
		}
	}

	public async Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings)
	{
		try
		{
			var result = context!.CompanyRegionalSettings!.Update(regionalSettings);
			await context.SaveChangesAsync();
			return TransactionResult<CompanyRegionalSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<CompanyRegionalSetup>.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateRegionalSettingsAsync), logger));
		}
	}

	public async Task<TransactionResult<CompanyBankAccount>> UpdateDefaultTaxAsync (CompanyTax defaultTax)
	{
		try
		{
			defaultTax.Default = true;
			context!.CompanyTaxes!.Update(defaultTax);
			await context!.SaveChangesAsync();
			return TransactionResult<CompanyBankAccount>.Success();
		}
		catch (Exception ex)
		{
			return TransactionResult<CompanyBankAccount>.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateDefaultTaxAsync), logger));
		}
	}

	public async Task<TransactionResult<CompanyTax>> UpdateTaxAsync (CompanyTax tax)
	{
		try
		{
			var result = context!.CompanyTaxes!.Update(tax);
			await context.SaveChangesAsync();
			return TransactionResult<CompanyTax>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<CompanyTax>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateTaxAsync), logger));
		}
	}

	public async Task<TransactionResult<CompanyImage>> UpdateLogoAsync (Company company, CompanyImage companyImage)
	{
		try
		{
			var image = await context!.CompanyImages!.AddAsync(companyImage);
			var companyLogo = await context.CompanyLogos!.FindAsync(company.Id!);

			if (companyLogo == null)
			{
				await context!.CompanyLogos!.AddAsync(new CompanyLogo(company.Id!, image.Entity.Id));
			}
			else
			{
				companyLogo.ImageId = image.Entity.CompanyId;
				context!.CompanyLogos!.Update(companyLogo);
			}

			await context!.SaveChangesAsync();
			return TransactionResult<CompanyImage>.Success(image.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<CompanyImage>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateLogoAsync), logger));
		}
	}

	public async Task<TransactionResult> UpdateMailSettingsAsync (CompanyMailSetup companyMailSettings)
	{
		try
		{
			context!.CompanyMailSettings!.Update(companyMailSettings);
			await context!.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateMailSettingsAsync), logger));
		}
	}

	public async Task<TransactionResult<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup)
	{
		try
		{
			var result = context.SupplierSetups!.Update(supplierSetup);
			await context.SaveChangesAsync();
			return TransactionResult<SupplierSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<SupplierSetup>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateSupplierSetupAsync), logger));
		}
	}

	public async Task<TransactionResult<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup)
	{
		try
		{
			var result = context.CustomerSetups!.Update(customerSetup);
			await context.SaveChangesAsync();
			return TransactionResult<CustomerSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<CustomerSetup>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateCustomerSetupAsync), logger));
		}
	}

	public async Task<TransactionResult<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup)
	{
		try
		{
			var result = context.ItemSetups!.Update(itemSetup);
			await context.SaveChangesAsync();
			return TransactionResult<ItemSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<ItemSetup>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateItemSetupAsync), logger));
		}
	}

	public async Task<TransactionResult<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup)
	{
		try
		{
			var result = context.DocumentSetups!.Update(documentSetup);
			await context.SaveChangesAsync();
			return TransactionResult<DocumentSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<DocumentSetup>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateDocumentSetupAsync), logger));
		}
	}

	public async Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount)
	{
		try
		{
			var result = context.BankAccounts!.Update(bankAccount);
			await context.SaveChangesAsync();
			return TransactionResult<BankAccount>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<BankAccount>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateBankAccountAsync), logger));
		}
	}
}
