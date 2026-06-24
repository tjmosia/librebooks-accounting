using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CustomerSpace;

namespace Librebooks.Areas.Customers.Providers;

public interface ICustomerManager
{
    Task<TransactionResult<CustomerAdjustment>> AddAdjustmentAsync(Customer customer, CustomerAdjustment adjustment);
    Task<TransactionResult<CustomerAdjustment>> UpdateAdjustmentAsync(CustomerAdjustment adjustment);
    Task<CustomerAdjustment?> FindAdjustmentByIdAsync(Customer customer, int id, CancellationToken cancellationToken = default);
    Task<IList<CustomerAdjustment>> GetAdjustmentsAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<TransactionResult> DeleteAdjustmentAsync(CustomerAdjustment adjustment);
}
