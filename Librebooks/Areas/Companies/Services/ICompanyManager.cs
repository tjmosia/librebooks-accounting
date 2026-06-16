using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.IdentitySpace;

namespace Librebooks.Areas.Companies.Services;

public interface ICompanyManager
{
	Task<TransactionResult<Company>> CreateASync (Company company, User user);
}
