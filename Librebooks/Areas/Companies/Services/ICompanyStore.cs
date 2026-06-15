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

namespace Librebooks.Areas.Companies.Services
{
	public interface ICompanyStore
	{
		/***********************************************************************************************************************************
         ****** SELECT TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<Company?> FindByIdAsync(int companyId, CancellationToken cancellationToken = default);
		Task<Company?> FindByIdAsync(int companyId, int userId, CancellationToken cancellationToken = default);
		Task<IList<Company?>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
		Task<CompanyRegionalSetup?> FindRegionalSettingsAsync(int companyId, CancellationToken cancellationToken = default);
		Task<CompanyImage?> FindLogoAsync(int companyId, CancellationToken cancellationToken = default);
		Task<IList<Tax>> FindTaxTypesAsync(int companyId, CancellationToken cancellationToken = default);
		Task<Tax?> FindTaxByIdAsync(int companyId, int taxTypeId, CancellationToken cancellationToken = default);
		Task<Tax?> FindDefaultTaxAsync(int companyId, CancellationToken cancellationToken = default);
		Task<CompanyMailSetup?> FindMailSettingsAsync(int companyId, CancellationToken cancellationToken = default);
		Task<BankAccount?> FindDefaultBankAccountAsync(int companyId, CancellationToken cancellationToken = default);
		Task<BankAccount?> FindBankAccountByIdAsync(int companyId, int bankAccountId, CancellationToken cancellationToken = default);
		Task<Contact?> FindSalesPersonByIdAsync(int companyId, int salesPersonId, CancellationToken cancellationToken = default);
		Task<Contact?> FindSalesPersonByUserIdAsync(int companyId, int userId, CancellationToken cancellationToken = default);
		Task<IList<User>> FindUsersAsync(int companyId, CancellationToken cancellationToken = default);

		/***********************************************************************************************************************************
         ****** INSERT TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<TransactionResult<Tax>> CreateTaxTypeAsync(Company company, Tax taxType);
		Task<TransactionResult<Contact>> CreateSalesPersonAsync(Company company, Contact contact);
		Task<TransactionResult<BankAccount>> CreateBankAccountAsync(Company company, BankAccount bankAccount);
		Task<TransactionResult<CompanyImage>> CreateLogoAsync(Company company, CompanyImage image);
		Task<TransactionResult<Company>> CreateAsync(Company company);

		/***********************************************************************************************************************************
         ****** UPDATE TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<Result<CompanyRegionalSetup>> UpdateRegionalSettingsAsync(CompanyRegionalSetup regionalSettings);
		Task<Result<Tax>> UpdateTaxAsync(Tax tax);
		Task<Result<CompanyImage>> UpdateLogoAsync(Company company, CompanyImage companyImage);
		Task<Result> UpdateMailSettingsAsync(CompanyMailSetup mailSettings);
		Task<Result<SupplierSetup>> UpdateSupplierSetupAsync(SupplierSetup supplierSetup);
		Task<Result<CustomerSetup>> UpdateCustomerSetupAsync(CustomerSetup customerSetup);
		Task<Result<ItemSetup>> UpdateItemSetupAsync(ItemSetup itemSetup);
		Task<Result<DocumentSetup>> UpdateDocumentSetupAsync(DocumentSetup documentSetup);
		Task<Result<BankAccount>> UpdateBankAccountAsync(BankAccount bankAccount);
		Task<Result<Company>> UpdateAsync(Company company);

		/***********************************************************************************************************************************
         ****** DELETE TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<TransactionResult> DeleteSalesPersonAsync(SalesPerson salesPerson);
		Task<TransactionResult> DeleteAsync(Company company);
		Task<TransactionResult> DeleteTaxTypeAsync(CompanyTax companyTaxType);
		Task<TransactionResult> DeleteBankAccountAsync(BankAccount bankAccount);
		Task<TransactionResult> DeleteLogoAsync(CompanyLogo companyLogo);
	}
}
