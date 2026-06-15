using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	public async Task<Result<Company>> UpdateAsync (Company company)
	{
		company.RefreshConcurrencyToken();

		try
		{
			var result = context.Companies!.Update(company);
			await context.SaveChangesAsync();
			return Result<Company>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<Company>.Failure();
			throw;
		}
	}

	public async Task<Result<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings)
	{
		try
		{
			var result = context!.CompanyRegionalSettings!.Update(regionalSettings);
			await context.SaveChangesAsync();
			return Result<CompanyRegionalSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<CompanyRegionalSetup>.Failure();
            throw;
        }
	}

	public async Task<Result<CompanyBankAccount>> UpdateDefaultTaxTypeAsync (CompanyTax defaultTaxType)
	{
		try
		{
			defaultTaxType.Default = true;
			context!.CompanyTaxes!.Update(defaultTaxType);
			await context!.SaveChangesAsync();
			return Result<CompanyBankAccount>.Success();
		}
		catch (Exception)
		{
			return Result<CompanyBankAccount>.Failure();
			throw;
		}
	}

	public async Task<Result<Tax>> UpdateTaxAsync (Tax tax)
	{
		try
		{
			var result = context!.Taxes!.Update(tax);
			await context.SaveChangesAsync();
			return Result<Tax>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<Tax>.Failure();
			throw;
		}
	}

	public async Task<Result<CompanyImage>> UpdateLogoAsync (Company company, CompanyImage companyImage)
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
			return Result<CompanyImage>.Success(image.Entity);
		}
		catch (Exception)
		{
			return Result<CompanyImage>.Failure();
			throw;
		}
	}

	public async Task<Result> UpdateMailSettingsAsync (CompanyMailSetup companyMailSettings)
	{
		try
		{
			context!.CompanyMailSettings!.Update(companyMailSettings);
			await context!.SaveChangesAsync();
			return Result.Success;
		}
		catch (Exception)
		{
			return Result.Failure();
			throw;
		}
	}

	public async Task<Result<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup)
	{
		try
		{
			var result = context.SupplierSetups!.Update(supplierSetup);
			await context.SaveChangesAsync();
			return Result<SupplierSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<SupplierSetup>.Failure();
			throw;
		}
	}

	public async Task<Result<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup)
	{
		try
		{
			var result = context.CustomerSetups!.Update(customerSetup);
			await context.SaveChangesAsync();
			return Result<CustomerSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<CustomerSetup>.Failure();
			throw;
		}
	}

	public async Task<Result<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup)
	{
		try
		{
			var result = context.ItemSetups!.Update(itemSetup);
			await context.SaveChangesAsync();
			return Result<ItemSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<ItemSetup>.Failure();
			throw;
		}
	}

	public async Task<Result<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup)
	{
		try
		{
			var result = context.DocumentSetups!.Update(documentSetup);
			await context.SaveChangesAsync();
			return Result<DocumentSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<DocumentSetup>.Failure();
			throw;
		}
	}

	public async Task<Result<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount)
	{
		try
		{
			var result = context.BankAccounts!.Update(bankAccount);
			await context.SaveChangesAsync();
			return Result<BankAccount>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<BankAccount>.Failure();
			throw;
		}

	}

}
