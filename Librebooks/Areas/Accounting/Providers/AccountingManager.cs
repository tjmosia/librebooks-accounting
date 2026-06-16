using Librebooks.Data;
using Librebooks.Providers.Stores;

namespace Librebooks.Areas.Accounting.Providers;

public class AccountingManager (AppDbContext context) : StoreBase(context), IAccountingManager
{


}
