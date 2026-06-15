using Librebooks.Core.Operations;

namespace Librebooks.Areas.Systems.Providers;

public interface ISystemsManager
{

	/******************************************************************
    * SYSTEM_COMPANY_NUMBER Store Manager Actions
    ******************************************************************/
	Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat, CancellationToken cancellationToken = default);
	Task<(string? Prefix, string? Surfix)> GetCompanyNumberParamsAsync (CancellationToken cancellationToken = default);
}
