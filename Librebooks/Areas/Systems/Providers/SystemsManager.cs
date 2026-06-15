using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Microsoft.Extensions.Caching.Distributed;

namespace Librebooks.Areas.Systems.Providers;

public class SystemsManager (SystemsStore systemStore, IDistributedCache cache, DbErrorDescriber? dbErrorDescriber, ILogger<SystemsManager> logger)
	: ISystemsManager
{
	private readonly SystemsStore store = systemStore;
	private readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;
	private readonly IDistributedCache cache = cache;
	private readonly ILogger<SystemsManager> logger = logger;

	private const string COMP_NUM_PREFIX = nameof(COMP_NUM_PREFIX);
	private const string COMP_NUM_FORMAT = nameof(COMP_NUM_FORMAT);

	/******************************************************************
    * SystemCompanyNumber Manager Actions
    ******************************************************************/

	public async Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat, CancellationToken cancellationToken = default)
	{
		var setup = await store.CompanyNumberStore.FindCurrentAsync(cancellationToken);

		setup!.Prefix = prefix;
		setup.NumberFormat = numberFormat;
		var result = await store.CompanyNumberStore.UpdateAsync(setup!, cancellationToken);

		return TransactionResult.Success;
	}

	public async Task<(string? Prefix, string? Surfix)> GetCompanyNumberParamsAsync (CancellationToken cancellationToken = default)
	{
		var setup = await store.GetCompanySetupAsync(cancellationToken);

		return (setup?.Prefix, setup?.Suffix);
	}

}
