using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

namespace Librebooks.Areas.Inventory.Providers;

public interface IItemStore
{
	/********************************************************************
	 * ITEM OPERATIONS
	 ********************************************************************/
	Task<TransactionResult<Item>> CreateAsync (Company company, Item item);
	Task<TransactionResult<Item>> UpdateAsync (Item item);
	Task<Item?> FindByCodeAsync (Company company, string itemCode, CancellationToken cancellationToken = default);
	Task<IList<Item>> FindAllAsync (Company company, CancellationToken cancellationToken = default);
	Task<Item?> FindByIdAsync (Company company, int itemId, CancellationToken cancellationToken = default);
	Task<TransactionResult> DeleteItemAsync (Item item);

	/********************************************************************
	 * ITEM ADJUSTMENTS OPERATIONS
	 ********************************************************************/
	Task<IList<ItemAdjustment>> FindAdjustmentsByItemAsync (Item item, CancellationToken cancellationToken = default);
	Task<IList<ItemAdjustment>> FindAdjustmentsAsync (Company company, CancellationToken cancellationToken = default);
	Task<ItemAdjustment?> FindAdjustmentByIdAsync (Company company, int adjustmentId, CancellationToken cancellationToken = default);
	Task<TransactionResult<ItemAdjustment>> CreateAdjustmentAsync (Item item, ItemAdjustment adjustment);
	Task<TransactionResult<ItemAdjustment>> UpdateAdjustmentAsync (ItemAdjustment adjustment);
	Task<TransactionResult> DeleteAdjustmentAsync (ItemAdjustment adjustment);

	/********************************************************************
	 * ITEM CATEGORIES
	 *******************************************************************/
	Task<IList<ItemCategory>> FindCategoriesAsync (Company company, CancellationToken cancellationToken = default);
	Task<ItemCategory?> FindCategoryByIdAsync (Company company, int categoryId, CancellationToken cancellationToken = default);
	Task<TransactionResult<ItemCategory>> CreateCategoryAsync (Company company, ItemCategory category);
	Task<TransactionResult<ItemCategory>> UpdateCategoryAsync (ItemCategory category);
	Task<TransactionResult> DeleteCategoryAsync (ItemCategory category);
}
