using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

namespace Librebooks.Areas.Inventory.Providers
{
	public interface IItemManager
	{
		Task<TransactionResult<Item>> AddAdjustmentAsync (Company company, Item item, ItemAdjustment adjustment);
		Task<TransactionResult<Item>> UpdateAdjustmentAsync (ItemAdjustment adjustment, Item item);
		Task<TransactionResult> DeleteAdjustmentAsync (ItemAdjustment adjustment);
	}
}
