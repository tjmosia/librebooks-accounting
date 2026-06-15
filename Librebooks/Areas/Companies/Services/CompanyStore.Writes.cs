using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	/***********************************************************************************************************************************
	****** INSERT TRANSACTIONS
	***********************************************************************************************************************************/

	public async Task<Result<Company>> CreateAsync (Company company, 
		CompanyRegionalSetup regionalSetup, 
		SupplierSetup supplierSetup, 
		CustomerSetup customerSetup,
		ItemSetup itemSetup,
		DocumentSetup documentSetup)
	{
		var result = await context!.AddAsync(company);
		regionalSetup.Company = result.Entity;
		supplierSetup.Company = result.Entity;
		customerSetup.Company = result.Entity;
		itemSetup.Company = result.Entity;
		documentSetup.Company = result.Entity;

        var taxes = await context.Taxes!.Where(p => p.System)
            .ToListAsync();

		var companyTaxes = taxes.Select(p => new CompanyTax
		{
			CompanyId = result.Entity.Id,
			TaxId = p.Id,
			Default = false
		}).ToList();

        await context.AddRangeAsync(regionalSetup, supplierSetup, customerSetup, itemSetup, documentSetup, companyTaxes, companyTaxes);
		context.SaveChanges();

		return Result<Company>.Success(result.Entity);
	}

	public async Task<Result<Tax>> CreateTaxTypeAsync (Company company, Tax taxType)
	{
		var result = await context.Taxes!.AddAsync(taxType);
		await context.CompanyTaxes!.AddAsync(new CompanyTax(company.Id, taxType.Id));
		await context.SaveChangesAsync();

		return Result<Tax>.Success(result.Entity);
	}

	public async Task<Result<Contact>> CreateSalesPersonAsync (Company company, Contact contact)
	{
		var result = await context.Contacts!.AddAsync(contact);

		await context.SalesPeople!.AddAsync(new SalesPerson
		{
			CompanyId = company.Id,
			ContactId = contact.Id,
		});

		await context.SaveChangesAsync();

		return Result<Contact>.Success(result.Entity);
	}

	public async Task<Result<BankAccount>> CreateBankAccountAsync (Company company, BankAccount bankAccount)
	{
		bankAccount.CompanyId = company.Id;
		var result = await context.BankAccounts!.AddAsync(bankAccount);
		await context.SaveChangesAsync();
		return Result<BankAccount>.Success(result.Entity);
	}

	public async Task<Result<BankAccount>> CreateDefaultBankAccountAsync (Company company, BankAccount bankAccount)
	{
		var result = await context.CompanyDefaultBankAccounts!
			.AddAsync(new CompanyBankAccount(company.Id, bankAccount.Id));
		await context.SaveChangesAsync();
		return Result<BankAccount>.Success(bankAccount);
	}

	public async Task<Result<CompanyImage>> CreateLogoAsync (Company company, CompanyImage image)
	{
			var result = await context.CompanyImages!.AddAsync(image);
			await context.SaveChangesAsync();
			await UpdateLogoAsync(company, result.Entity);
			return Result<CompanyImage>.Success(result.Entity);
	}
}
