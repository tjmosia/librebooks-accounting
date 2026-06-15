using Librebooks.Areas.Accounting.Providers;
using Librebooks.Areas.Systems.Providers;
using Librebooks.Core.Constants;
using Librebooks.Core.Operations;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Providers.Stores;

namespace Librebooks.Areas.Companies.Services;

public class CompanyManager (ICompanyStore store, SystemsStore systemsStore, DocumentSetupStore documentSetupStore, LedgerAccountStore? ledgerAccountStore) : ICompanyManager
{
	private readonly ICompanyStore store = store;
	private readonly SystemsStore systemsStore = systemsStore;
	private readonly DocumentSetupStore documentSetupStore = documentSetupStore;
	private readonly LedgerAccountStore? ledgerAccountStore = ledgerAccountStore;
	public async Task<TransactionResult<Company>> CreateASync (Company company, User user)
	{
		company.CustomerSetup = new CustomerSetup
		{
			Prefix = "CUS",
			NextNumber = 1
		};

		company.SupplierSetup = new SupplierSetup
		{
			Prefix = "SUP",
			NextNumber = 1,
			NumberFormat = "0000",
		};

		company.ItemSetup = new ItemSetup
		{
			Prefix = "ITM",
			NextNumber = 1
		};

		company.RegionalSetup = new CompanyRegionalSetup
		{
			DateFormat = await systemsStore.DateFormatsStore.FindDefaultAsync(),
			Currency = await systemsStore.CurrenciesStore.FindDefaultAsync(),
			DecimalMark = ".",
			ThousandsSeperator = " ",
			RoundToNearest = 2
		};

		company.DocumentSetups = [..(await documentSetupStore.FindAllAsync()).Select( p=> new DocumentSetup
		{
			FooterMessage = p.FooterMessage,
			NoteMessage = p.NoteMessage,
			Prefix = p.Prefix,
			NextNumber = p.NextNumber,
			Suffix = p.Suffix,
			System = false,
			TypeId = p.TypeId,
			PrintTemplateId = p.PrintTemplateId,
			Title = p.Title
		})];

		company.Users = [new CompanyUser{
			UserId = user.Id
		}];

		company.Taxes = [..(await systemsStore.TaxTypesStore.FindAllAsync()).Select(p => new CompanyTax
		{
			Tax = p,
			Default = company.VATNumber == null && p.Type!.Equals(TaxCodeTypes.ZeroVAT) || p.Type!.Equals(TaxCodeTypes.StandardVAT)
		})];

		company.ChartOfAccounts = [.. (await ledgerAccountStore!.GetSystemLedgerAccountsAsync()).Select(p=> new CompanyLedgerAccount {
			Account = p,
			Balance = 0
		})];

		return await store.CreateAsync(company);

	}
}
