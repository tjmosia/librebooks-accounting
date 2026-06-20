using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Companies.Services;

public interface ICompanyManager
{
    Task<TransactionResult<Company>> CreateAsync(Company company, User user, Country country, Currency currency);
}
