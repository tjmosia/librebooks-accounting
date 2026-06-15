using Librebooks.Areas.Systems.Providers;
using Librebooks.Data;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore (AppDbContext context, ILogger<CompanyStore> logger, ISystemsManager sysManager)
	: ICompanyStore
{
	private readonly ISystemsManager systemManager = sysManager;
	private readonly AppDbContext context = context;
	private readonly ILogger<CompanyStore> logger = logger;

	public async Task<Company?> FindByIdAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.Companies!
			.Where(p => p.Id == companyId)
			.Include(p => p.Logo)
				.ThenInclude(p => p!.Image)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Company?> FindByIdAsync (int companyId, int userId, CancellationToken cancellationToken = default)
	{
		return await context.CompanyUsers!.Where(p => p.CompanyId == companyId && p.UserId == userId)
			.Include(p => p.Company)
			.ThenInclude(p => p!.Logo)
				.ThenInclude(p => p!.Image)
			.Select(p => p.Company)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IList<Company?>> FindByUserIdAsync (int userId, CancellationToken cancellationToken = default)
	{
		return await context.CompanyUsers!.Where(p => p.UserId == userId)
			.Include(p => p.Company)
			.ThenInclude(p => p!.Logo)
				.ThenInclude(p => p!.Image)
			.Select(p => p.Company)
			.ToListAsync(cancellationToken);
	}

	public async Task<CompanyRegionalSetup?> GetRegionalSettingsAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.CompanyRegionalSettings!
			.FindAsync([company.Id], cancellationToken);

	public async Task<CompanyImage?> GetLogoAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.CompanyLogos!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.Image)
			.Select(p => p.Image)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<IList<Tax>> GetTaxesAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.CompanyTaxes!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.Tax)
			.Select(p => p.Tax!)
			.ToListAsync(cancellationToken);

	public async Task<Tax?> FindTaxByIdAsync (Company company, int taxTypeId, CancellationToken cancellationToken = default)
		=> await context!.CompanyTaxes!
			.Where(p => p.CompanyId == company.Id && p.TaxId == taxTypeId)
			.Include(p => p.Tax)
			.Select(p => p.Tax)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Tax?> GetDefaultTaxAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.CompanyTaxes!
			.Where(p => p.CompanyId == company.Id && p.Default)
			.Include(p => p.Tax)
			.Select(p => p.Tax)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<CompanyMailSetup?> GetMailSettingsAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.CompanyMailSettings!
			.FindAsync([company.Id], cancellationToken);

	public async Task<BankAccount?> GetDefaultBankAccountAsync (Company company, CancellationToken cancellationToken = default)
	 => await context!.CompanyDefaultBankAccounts!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.BankAccount)
			.Select(p => p.BankAccount)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<BankAccount?> FindBankAccountByIdAsync (Company company, int bankAccountId, CancellationToken cancellationToken = default)
		=> await context!.BankAccounts!
			.Where(p => p.CompanyId == company.Id && bankAccountId == p.Id)
			.FirstOrDefaultAsync(cancellationToken);
	public async Task<IList<BankAccount>> GetBankAccountsAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.BankAccounts!
			.Where(p => p.CompanyId == company.Id)
			.ToListAsync(cancellationToken);

	public async Task<Contact?> FindSalesPersonByIdAsync (Company company, int salesPersonId, CancellationToken cancellationToken = default)
		=> await context!.SalesPeople!
			.Where(p => p.CompanyId == company.Id && p.ContactId == salesPersonId)
			.Include(p => p.Contact)
			.Select(p => p.Contact)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Contact?> FindSalesPersonByUserIdAsync (Company company, int userId, CancellationToken cancellationToken = default)
		=> await context!.SalesPeople!
			.Where(p => p.CompanyId == company.Id && p.CompanyUserId == userId)
			.Include(p => p.Contact)
			.Select(p => p.Contact)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<IList<User>> GetUsersAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.CompanyUsers!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.User)
			.Select(p => p.User!)
			.ToListAsync(cancellationToken);
}
