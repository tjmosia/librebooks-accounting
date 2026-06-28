using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Data;

public class AppDbContext :
	IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
	public AppDbContext (DbContextOptions<AppDbContext> options)
		: base(options) { }

	public AppDbContext () { }

	/************************************************************************************************
         * Company Space
         ************************************************************************************************/
	public DbSet<Company>? Companies { get; set; }
	public DbSet<CompanyUser>? CompanyUsers { get; set; }
	public DbSet<CompanyBankAccount>? CompanyDefaultBankAccounts { get; set; }
	public DbSet<CompanyTax>? CompanyTaxes { get; set; }
	public DbSet<CompanyMailSetup>? CompanyMailSettings { get; set; }
	public DbSet<CompanyLogo>? CompanyLogos { get; set; }
	public DbSet<CompanyImage>? CompanyImages { get; set; }

	/************************************************************************************************
         * Customer Space
         ************************************************************************************************/
	public DbSet<Customer>? Customers { get; set; }
	public DbSet<CustomerAccountsContact>? CustomerAccountsContacts { get; set; }
	public DbSet<CustomerAdjustment>? CustomerAdjustments { get; set; }
	public DbSet<CustomerCategory>? CustomerCategories { get; set; }
	public DbSet<CustomerContact>? CustomerContacts { get; set; }
	public DbSet<CustomerNote>? CustomerNotes { get; set; }
	public DbSet<CustomerSetup>? CustomerSetups { get; set; }
	public DbSet<CompanyRegionalSetup>? CompanyRegionalSettings { get; set; }

	/************************************************************************************************
         * Sales Space
         ************************************************************************************************/
	public DbSet<SalesPerson>? SalesPeople { get; set; }
	public DbSet<SalesDocument>? SalesDocuments { get; set; }
	public DbSet<SalesDocumentLine>? SalesDocumentLines { get; set; }
	public DbSet<SalesInvoiceReceipt>? SalesInvoiceReceipts { get; set; }
	public DbSet<SalesOrderInvoice>? SalesOrderInvoices { get; set; }
	public DbSet<SalesQuoteOrder>? SalesQuoteOrders { get; set; }
	public DbSet<SalesCredit>? SalesCredits { get; set; }
	public DbSet<SalesReceipt>? SalesReceipts { get; set; }
	public DbSet<SalesLine>? SalesLines { get; set; }
	public DbSet<SalesDocumentCustomerDetails>? SalesDocumentCustomerDetails { get; set; }
	public DbSet<SalesInvoiceCredit>? SalesInvoiceCredits { get; set; }
	public DbSet<SalesInvoiceWriteoff>? SalesInvoiceWriteoffs { get; set; }
	public DbSet<SalesQuoteProForma>? SalesQuoteProFormas { get; set; }
	public DbSet<SalesLedger>? SalesLedgers { get; set; }

	/************************************************************************************************
         * Inventory Space
         ************************************************************************************************/
	public DbSet<Item>? Items { get; set; }
	public DbSet<ItemSetup>? ItemSetups { get; set; }
	public DbSet<ItemAdjustment>? ItemAdjustments { get; set; }
	public DbSet<ItemCategory>? ItemCategories { get; set; }
	public DbSet<ItemInventory>? ItemInventories { get; set; }
	public DbSet<ItemDetail>? ItemDetails { get; set; }


	/************************************************************************************************
         * Accounting Space
         ************************************************************************************************/
	public DbSet<LedgerAccount>? LedgerAccounts { get; set; }
	public DbSet<LedgerAccountCategory>? LedgerAccountCategories { get; set; }
	public DbSet<JournalLine>? JournalEntries { get; set; }
	public DbSet<LedgerAccountCashFlowType>? LedgerAccountCashFlowTypes { get; set; }
	public DbSet<CompanyLedgerAccount>? CompanyLedgerAccounts { get; set; }

	/************************************************************************************************
         * Banking Space
         ************************************************************************************************/
	public DbSet<BankAccount>? BankAccounts { get; set; }
	public DbSet<BankAccountCategory>? BankAccountCategories { get; set; }

	/************************************************************************************************
         * Document Space
         ************************************************************************************************/
	public DbSet<DocumentSetup>? DocumentSetups { get; set; }
	public DbSet<DocumentStatus>? DocumentStatuses { get; set; }
	public DbSet<DocumentPrintTemplate>? DocumentPrintTemplates { get; set; }
	public DbSet<DocumentCompanyDetails>? DocumentCompanyDetails { get; set; }
	public DbSet<DocumentType>? DocumentTypes { get; set; }

	/************************************************************************************************
         * System Space
         ************************************************************************************************/
	public DbSet<ShippingTerm>? ShippingTerms { get; set; }
	public DbSet<ShippingMethod>? ShippingMethods { get; set; }
	public DbSet<Country>? Countries { get; set; }
	public DbSet<Currency>? Currencies { get; set; }
	public DbSet<DateFormat>? DateFormats { get; set; }
	public DbSet<Tax>? Taxes { get; set; }
	public DbSet<PaymentMethod>? PaymentMethods { get; set; }
	public DbSet<PaymentTerm>? PaymentTerms { get; set; }
	public DbSet<CompanySetup>? CompanySetups { get; set; }
	public DbSet<BusinessSector>? BusinessSectors { get; set; }

	/************************************************************************************************
         * Supplier Space
         ************************************************************************************************/
	public DbSet<Supplier>? Suppliers { get; set; }
	public DbSet<SupplierNote>? SupplierNotes { get; set; }
	public DbSet<SupplierAccountsContact>? SupplierAccountsContacts { get; set; }
	public DbSet<SupplierAdjustment>? SupplierAdjustments { get; set; }
	public DbSet<SupplierCategory>? SupplierCategories { get; set; }
	public DbSet<SupplierContact>? SupplierContacts { get; set; }
	public DbSet<SupplierSetup>? SupplierSetups { get; set; }

	/************************************************************************************************
         * Purchases Space
         ************************************************************************************************/
	public DbSet<PurchaseDocument>? PurchaseDocuments { get; set; }
	public DbSet<PurchaseDocumentLine>? PurchaseDocumentLines { get; set; }
	public DbSet<PurchaseBuyer>? PurchaseBuyers { get; set; }
	public DbSet<PurchaseCreditInvoice>? PurchaseCreditInvoices { get; set; }
	public DbSet<PurchaseLine>? PurchaseLines { get; set; }
	public DbSet<PurchaseInvoiceReturn>? PurchaseReturnInvoices { get; set; }
	public DbSet<PurchasePayment>? PurchaseReceipts { get; set; }
	public DbSet<PurchaseOrderInvoice>? PurchaseOrderInvoices { get; set; }
	public DbSet<PurchaseInvoicePayment>? PurchaseInvoiceReceipts { get; set; }
	public DbSet<DocumentSupplierDetail>? DocumentSupplierDetails { get; set; }
	public DbSet<PurchaseLedger>? PurchaseLedgers { get; set; }
	public DbSet<PurchaseLedgerJournal>? PurchaseLedgerJournals { get; set; }

	/************************************************************************************************
         * GENERAL SPACE
         ************************************************************************************************/
	public DbSet<Contact>? Contacts { get; set; }
	public DbSet<Note>? Notes { get; set; }
	public DbSet<VerificationRequest>? VerificationRequests { get; set; }

	protected override void OnModelCreating (ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		/************************************************************************************************
             * User Space
             ************************************************************************************************/
		User.OnModelCreating(builder);
		Role.OnModelCreating(builder);
		RoleClaim.OnModelCreating(builder);
		UserRole.OnModelCreating(builder);
		UserToken.OnModelCreating(builder);
		UserLogin.OnModelCreating(builder);
		UserClaim.OnModelCreating(builder);

		/************************************************************************************************
             * Inventory Space
             ************************************************************************************************/
		Item.OnModelCreating(builder);
		ItemCategory.OnModelCreating(builder);
		ItemInventory.OnModelCreating(builder);
		ItemAdjustment.OnModelCreating(builder);
		ItemSetup.OnModelCreating(builder);
		ItemDetail.OnModelCreating(builder);

		/************************************************************************************************
             * Document Space
             ************************************************************************************************/
		Models.Entity.DocumentSpace.DocumentCompanyDetails.OnModelCreating(builder);
		DocumentPrintTemplate.OnModelCreating(builder);
		DocumentSetup.OnModelCreating(builder);
		DocumentStatus.OnModelCreating(builder);
		DocumentType.OnModelCreating(builder);

		/************************************************************************************************
             * Sales Space
             ************************************************************************************************/
		Models.Entity.SalesSpace.SalesDocumentCustomerDetails.OnModelCreating(builder);
		SalesCredit.OnModelCreating(builder);
		SalesDocument.OnModelCreating(builder);
		SalesDocumentLine.OnModelCreating(builder);
		SalesInvoiceCredit.OnModelCreating(builder);
		SalesInvoiceReceipt.OnModelCreating(builder);
		SalesInvoiceWriteoff.OnModelCreating(builder);
		SalesLine.OnModelCreating(builder);
		SalesOrderInvoice.OnModelCreating(builder);
		SalesPerson.OnModelCreating(builder);
		SalesQuoteOrder.OnModelCreating(builder);
		SalesReceipt.OnModelCreating(builder);
		SalesQuoteProForma.OnModelCreating(builder);
		SalesLedger.OnModelCreating(builder);
		SalesLedgerJournal.OnModelCreating(builder);

		/************************************************************************************************
             * Company Space
             ************************************************************************************************/
		Company.OnModelCreating(builder);
		CompanyBankAccount.OnModelCreating(builder);
		CompanyImage.OnModelCreating(builder);
		CompanyLogo.OnModelCreating(builder);
		CompanyMailSetup.OnModelCreating(builder);
		CompanyRegionalSetup.OnModelCreating(builder);
		CompanySetup.OnModelCreating(builder);
		CompanyTax.BuildModel(builder);
		CompanyUser.OnModelCreating(builder);

		/************************************************************************************************
             * System Space
             ************************************************************************************************/
		Country.OnModelCreating(builder);
		Currency.OnModelCreating(builder);
		DateFormat.OnModelCreating(builder);
		Tax.OnModelCreating(builder);
		ShippingMethod.OnModelCreating(builder);
		ShippingTerm.OnModelCreating(builder);
		PaymentMethod.OnModelCreating(builder);
		BusinessSector.OnModelCreating(builder);

		/************************************************************************************************
             * Customer Space
             ************************************************************************************************/
		Customer.OnModelCreating(builder);
		CustomerAccountsContact.OnModelCreating(builder);
		CustomerAdjustment.OnModelCreating(builder);
		CustomerCategory.OnModelCreating(builder);
		CustomerContact.BuildModel(builder);
		CustomerNote.OnModelCreating(builder);
		CustomerSetup.OnModelCreating(builder);
		CustomerWriteOff.OnModelCreating(builder);

		/************************************************************************************************
             * Accounting Space
             ************************************************************************************************/
		LedgerAccount.OnModelCreating(builder);
		LedgerAccountCategory.OnModelCreating(builder);
		LedgerAccountCashFlowType.OnModelCreating(builder);
		Journal.OnModelCreating(builder);
		JournalLine.OnModelCreating(builder);
		CompanyLedgerAccount.OnModelCreating(builder);

		/************************************************************************************************
             * Supplier Space
             ************************************************************************************************/
		Supplier.OnModelCreating(builder);
		SupplierNote.OnModelCreating(builder);
		SupplierAdjustment.OnModelCreating(builder);
		SupplierContact.OnModelCreating(builder);
		SupplierAccountsContact.OnModelCreating(builder);
		SupplierCategory.OnModelCreating(builder);
		SupplierSetup.OnModelCreating(builder);

		/************************************************************************************************
             * Purchasing Space
             ************************************************************************************************/
		DocumentSupplierDetail.OnModelCreating(builder);
		PurchaseBuyer.OnModelCreating(builder);
		PurchaseDocument.OnModelCreating(builder);
		PurchaseDocumentLine.OnModelCreating(builder);
		PurchaseCreditInvoice.OnModelCreating(builder);
		PurchaseInvoicePayment.OnModelCreating(builder);
		PurchaseInvoiceReturn.OnModelCreating(builder);
		PurchaseLine.OnModelCreating(builder);
		PurchaseOrderInvoice.OnModelCreating(builder);
		PurchasePayment.OnModelCreating(builder);
		PurchaseLedger.OnModelCreating(builder);
		PurchaseLedgerJournal.OnModelCreating(builder);

		/************************************************************************************************
             * Banking Space
             ************************************************************************************************/
		BankAccount.OnModelCreating(builder);
		BankAccountCategory.OnModelCreating(builder);

		/************************************************************************************************
             * General Space
             ************************************************************************************************/
		Contact.OnModelCreating(builder);
		Note.OnModelCreating(builder);
		VerificationRequest.OnModelCreating(builder);
	}
}
