using LibrebooksBlazor.Core.Operations;
using LibrebooksBlazor.Models.Entity.BankingSpace;
using LibrebooksBlazor.Models.Entity.CompanySpace;
using LibrebooksBlazor.Models.Entity.GeneralSpace;
using LibrebooksBlazor.Models.Entity.SalesSpace;
using LibrebooksBlazor.Models.Entity.SystemSpace;

namespace LibrebooksBlazor.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	/***********************************************************************************************************************************
	****** INSERT TRANSACTIONS
	***********************************************************************************************************************************/

	public async Task<TransactionResult<Company>> CreateAsync (Company company)
	{
		try
		{
			var result = await context!.AddAsync(company);
			context.SaveChanges();
			return TransactionResult<Company>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error with Exception occurred while trying to create Company:*** \n\n{message}", ex.Message);
			return TransactionResult<Company>.Failure();
		}
	}

	public async Task<TransactionResult<Tax>> CreateTaxTypeAsync (Company company, Tax taxType)
	{
		try
		{
			var result = await context.Taxes!.AddAsync(taxType);
			await context.CompanyTaxes!.AddAsync(new CompanyTax(company.Id, taxType.Id));
			await context.SaveChangesAsync();

			return TransactionResult<Tax>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company TaxType:*** \n\n{message}", ex.Message);
			return TransactionResult<Tax>.Failure();
		}
	}

	public async Task<TransactionResult<Contact>> CreateSalesPersonAsync (Company company, Contact contact)
	{
		try
		{
			var result = await context.Contacts!.AddAsync(contact);

			await context.SalesPeople!.AddAsync(new SalesPerson
			{
				CompanyId = company.Id,
				ContactId = contact.Id,
			});

			await context.SaveChangesAsync();

			return TransactionResult<Contact>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company TaxType:*** \n\n{message}", ex.Message);
			return TransactionResult<Contact>.Failure();
		}
	}

	public async Task<TransactionResult<BankAccount>> CreateBankAccountAsync (Company company, BankAccount bankAccount)
	{
		try
		{
			bankAccount.CompanyId = company.Id;
			var result = await context.BankAccounts!.AddAsync(bankAccount);
			await context.SaveChangesAsync();
			return TransactionResult<BankAccount>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company Bank Account:*** \n\n{message}", ex.Message);
			return TransactionResult<BankAccount>.Failure();
		}
	}

	public async Task<TransactionResult<BankAccount>> CreateDefaultBankAccountAsync (Company company, BankAccount bankAccount)
	{
		try
		{
			var result = await context.CompanyDefaultBankAccounts!
				.AddAsync(new CompanyBankAccount(company.Id, bankAccount.Id));
			await context.SaveChangesAsync();
			return TransactionResult<BankAccount>.Success(bankAccount);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company Default Bank Account:*** \n\n{message}", ex.Message);
			return TransactionResult<BankAccount>.Failure();
		}
	}

	public async Task<TransactionResult<CompanyImage>> CreateLogoAsync (Company company, CompanyImage image)
	{
		try
		{
			var result = await context.CompanyImages!.AddAsync(image);
			await context.SaveChangesAsync();
			await UpdateLogoAsync(result.Entity);
			return TransactionResult<CompanyImage>.Success(result.Entity);
		}
		catch (Exception)
		{
			return TransactionResult<CompanyImage>.Failure();
		}
	}


}
