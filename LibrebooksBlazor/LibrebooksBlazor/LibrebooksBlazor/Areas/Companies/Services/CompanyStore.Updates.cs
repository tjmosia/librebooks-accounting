using LibrebooksBlazor.Core.Operations;
using LibrebooksBlazor.Models.Entity.BankingSpace;
using LibrebooksBlazor.Models.Entity.CompanySpace;
using LibrebooksBlazor.Models.Entity.CustomerSpace;
using LibrebooksBlazor.Models.Entity.DocumentSpace;
using LibrebooksBlazor.Models.Entity.InventorySpace;
using LibrebooksBlazor.Models.Entity.SupplierSpace;
using LibrebooksBlazor.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	public async Task<TransactionResult<Company>> UpdateAsync (Company company, CancellationToken cancellation = default)
	{
		company.RefreshConcurrencyToken();

		try
		{
			var result = context.Companies!.Update(company);
			await context.SaveChangesAsync(cancellation);
			return TransactionResult<Company>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<TransactionError> errors = [];

			if (ex is DbUpdateConcurrencyException)
			{
				errors.Add(TransactionError.Create("", "Data has already changed. Please try again."));
			}

			if (ex is DbUpdateException)
			{
				errors.Add(TransactionError.Create("", "Unable to update company. Please try again later."));
			}

			return TransactionResult<Company>.Failure([.. errors]);
		}
	}

	public async Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.CompanyRegionalSettings!.Update(regionalSettings);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<CompanyRegionalSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while trying to update CompanyRegionalSettings:*** \n\n{message}", ex.Message);
			return TransactionResult<CompanyRegionalSetup>.Failure();
		}
	}

	public async Task<TransactionResult<CompanyBankAccount>> UpdateDefaultTaxTypeAsync (CompanyTax defaultTaxType, CancellationToken cancellationToken = default)
	{
		try
		{
			defaultTaxType.Default = true;
			context!.CompanyTaxes!.Update(defaultTaxType);
			await context!.SaveChangesAsync(cancellationToken);
			return TransactionResult<CompanyBankAccount>.Success();
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to update CompanyDefaultTaxType:*** \n\n{message}", ex.Message);
			return TransactionResult<CompanyBankAccount>.Failure();
		}
	}

	public async Task<TransactionResult<Tax>> UpdateTaxAsync (Tax tax, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.Taxes!.Update(tax);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<Tax>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to update CompanyDefaultTaxType:*** \n\n{message}", ex.Message);
			return TransactionResult<Tax>.Failure();
		}
	}

	public async Task<TransactionResult<CompanyImage>> UpdateLogoAsync (CompanyImage companyImage, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.CompanyImages!.AddAsync(companyImage);
			var companyLogo = await context.CompanyLogos!.FindAsync(companyImage.CompanyId!);

			if (companyLogo == null)
			{
				await context!.CompanyLogos!.AddAsync(new CompanyLogo(companyImage.CompanyId!, companyImage.Id));
			}
			else
			{
				companyLogo.ImageId = companyImage.CompanyId;
				context!.CompanyLogos!.Update(companyLogo);
			}

			await context!.SaveChangesAsync(cancellationToken);
			return TransactionResult<CompanyImage>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to Update Company Logo:*** \n\n{message}", ex.Message);
			return TransactionResult<CompanyImage>.Failure();
		}
	}

	public async Task<TransactionResult> UpdateMailSettingsAsync (CompanyMailSetup companyMailSettings, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.CompanyMailSettings!.Update(companyMailSettings);
			await context!.SaveChangesAsync(cancellationToken);
			return TransactionResult.Success;
		}
		catch (Exception)
		{
			return TransactionResult.Failure();
		}
	}

	public async Task<TransactionResult<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.SupplierSetups!.Update(supplierSetup);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<SupplierSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<SupplierSetup>.Failure();
		}
	}

	public async Task<TransactionResult<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.CustomerSetups!.Update(customerSetup);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<CustomerSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<CustomerSetup>.Failure();
		}
	}

	public async Task<TransactionResult<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.ItemSetups!.Update(itemSetup);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<ItemSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<ItemSetup>.Failure();
		}
	}

	public async Task<TransactionResult<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.DocumentSetups!.Update(documentSetup);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<DocumentSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<DocumentSetup>.Failure();
		}
	}

	public async Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.BankAccounts!.Update(bankAccount);
			await context.SaveChangesAsync(cancellationToken);
			return TransactionResult<BankAccount>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<BankAccount>.Failure();
		}

	}

}
