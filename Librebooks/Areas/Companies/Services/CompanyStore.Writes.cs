using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Companies.Services;

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

			return TransactionResult<Company>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(CreateAsync), logger));
		}
	}

	public async Task<TransactionResult<CompanyTax>> AddTaxesAsync (Company company, Tax tax)
	{
		try
		{
			var result = await context.CompanyTaxes!.AddAsync(new CompanyTax(company.Id, tax.Id));
			await context.SaveChangesAsync();

			return TransactionResult<CompanyTax>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<CompanyTax>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(AddTaxesAsync), logger));
		}
	}

	public async Task<TransactionResult<Contact>> AddSalesPersonAsync (Company company, Contact contact)
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

			return TransactionResult<Contact>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(AddSalesPersonAsync), logger));
		}
	}

	public async Task<TransactionResult<BankAccount>> AddBankAccountAsync (Company company, BankAccount bankAccount)
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

			return TransactionResult<BankAccount>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(AddBankAccountAsync), logger));
		}
	}

	public async Task<TransactionResult<BankAccount>> AddDefaultBankAccountAsync (Company company, BankAccount bankAccount)
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

			return TransactionResult<BankAccount>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(AddDefaultBankAccountAsync), logger));
		}
	}

	public async Task<TransactionResult<CompanyImage>> AddLogoAsync (Company company, CompanyImage image)
	{
		try
		{
			var result = await context.CompanyImages!.AddAsync(image);
			await context.SaveChangesAsync();
			await UpdateLogoAsync(company, result.Entity);
			return TransactionResult<CompanyImage>.Success(result.Entity);
		}
		catch (Exception ex)
		{

			return TransactionResult<CompanyImage>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(AddLogoAsync), logger));
		}
	}
}
