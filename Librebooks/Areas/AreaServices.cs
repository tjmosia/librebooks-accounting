using Librebooks.Areas.Accounting.Providers;
using Librebooks.Areas.Companies.Services;
using Librebooks.Areas.Inventory.Providers;
using Librebooks.Areas.Systems.Providers;


//using Librebooks.Areas.Accounting.Services;
//using Librebooks.Areas.Customers.Services;
using Librebooks.Core.EFCore;

namespace Librebooks.Areas;

public static class AreaServices
{
	public static void ConfigureAll (IServiceCollection services)
	{
		services.AddSingleton<DbErrorDescriber>();
		services.AddScoped<ISystemsStore, SystemsStore>();
		services.AddScoped<ISystemsManager, SystemsManager>();
		services.AddScoped<ItemStore>();
		services.AddScoped<IItemManager, ItemManager>();

		services.AddScoped<ICompanyStore, CompanyStore>();
		services.AddScoped<ICompanyManager, CompanyManager>();

		services.AddScoped<IAccountsStore, AccountsStore>();
		services.AddScoped<IAccountingManager, AccountingManager>();
	}
}
