using Librebooks.Core.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Companies.Services;

public interface ICompanyStore
{
	/***********************************************************************************************************************************
         ****** SELECT TRANSACTIONS
         ***********************************************************************************************************************************/
	Task<Company?> FindByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<Company?> FindByIdAsync (int companyId, int userId, CancellationToken cancellationToken = default);
	Task<IList<Company?>> FindByUserIdAsync (int userId, CancellationToken cancellationToken = default);
	Task<CompanyRegionalSetup?> FindRegionalSettingsAsync (Company company, CancellationToken cancellationToken = default);
	Task<CompanyImage?> FindLogoAsync (Company company, CancellationToken cancellationToken = default);
	Task<IList<Tax>> FindTaxesAsync (Company company, CancellationToken cancellationToken = default);
	Task<Tax?> FindTaxByIdAsync (Company company, int taxId, CancellationToken cancellationToken = default);
	Task<Tax?> FindDefaultTaxAsync (Company company, CancellationToken cancellationToken = default);
	Task<CompanyMailSetup?> FindMailSettingsAsync (Company company, CancellationToken cancellationToken = default);
	Task<BankAccount?> FindDefaultBankAccountAsync (Company company, CancellationToken cancellationToken = default);
	Task<BankAccount?> FindBankAccountByIdAsync (Company company, int bankAccountId, CancellationToken cancellationToken = default);
	Task<Contact?> FindSalesPersonByIdAsync (Company company, int salesPersonId, CancellationToken cancellationToken = default);
	Task<Contact?> FindSalesPersonByUserIdAsync (Company company, int userId, CancellationToken cancellationToken = default);
	Task<IList<User>> FindUsersAsync (Company company, CancellationToken cancellationToken = default);

	/***********************************************************************************************************************************
         ****** INSERT TRANSACTIONS
         ***********************************************************************************************************************************/
	Task<TransactionResult<CompanyTax>> AddTaxesAsync (Company company, Tax tax);
	Task<TransactionResult<Contact>> AddSalesPersonAsync (Company company, Contact contact);
	Task<TransactionResult<BankAccount>> AddBankAccountAsync (Company company, BankAccount bankAccount);
	Task<TransactionResult<CompanyImage>> AddLogoAsync (Company company, CompanyImage image);
	Task<TransactionResult<Company>> CreateAsync (Company company);

	/***********************************************************************************************************************************
	****** UPDATE TRANSACTIONS
	***********************************************************************************************************************************/
	Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings);
	Task<TransactionResult<CompanyTax>> UpdateTaxAsync (CompanyTax tax);
	Task<TransactionResult<CompanyImage>> UpdateLogoAsync (Company company, CompanyImage companyImage);
	Task<TransactionResult> UpdateMailSettingsAsync (CompanyMailSetup mailSettings);
	Task<TransactionResult<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup);
	Task<TransactionResult<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup);
	Task<TransactionResult<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup);
	Task<TransactionResult<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup);
	Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount);
	Task<TransactionResult<Company>> UpdateAsync (Company company);

	/***********************************************************************************************************************************
         ****** DELETE TRANSACTIONS
         ***********************************************************************************************************************************/
	Task<TransactionResult> DeleteSalesPersonAsync (SalesPerson salesPerson);
	Task<TransactionResult> DeleteAsync (Company company);
	Task<TransactionResult> DeleteTaxTypeAsync (CompanyTax companyTaxType);
	Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount);
	Task<TransactionResult> DeleteLogoAsync (CompanyLogo companyLogo);
}
