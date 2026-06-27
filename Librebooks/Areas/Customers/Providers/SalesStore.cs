using Librebooks.Core.EFCore;
using Librebooks.Data;

namespace Librebooks.Areas.Customers.Providers
{
    public class SalesStore (AppDbContext context): DbStoreBase(context), ISalesStore
    {

    }
}
