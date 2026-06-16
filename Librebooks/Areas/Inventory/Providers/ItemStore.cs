using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Inventory.Providers;

public sealed class ItemStore (AppDbContext context, ILogger<ItemStore> logger) : IItemStore
{
	private readonly AppDbContext context = context;
	private readonly ILogger<ItemStore> logger = logger;

	/********************************************************************
	 * ITEM OPERATIONS
	 *******************************************************************/
	public async Task<TransactionResult<Item>> CreateAsync (Company company, Item item)
	{
		try
		{
			item.CompanyId = company.Id;
			var result = await context!.Items!.AddAsync(item);
			await context.SaveChangesAsync();
			return TransactionResult<Item>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<Item>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(CreateAsync), logger));
		}
	}

	public async Task<TransactionResult<Item>> UpdateAsync (Item item)
	{
		try
		{
			var result = context!.Items!.Update(item);
			await context.SaveChangesAsync();
			return TransactionResult<Item>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<Item>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateAsync), logger));
		}
	}

	public async Task<Item?> FindByCodeAsync (Company company, string itemCode, CancellationToken cancellationToken = default)
		=> await context!.Items!
			.Where(p => p.Code == itemCode && p.CompanyId == company.Id)
			.Include(p => p.Inventory)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<IList<Item>> FindAllAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.Items!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.Inventory)
			.ToListAsync(cancellationToken);

	public async Task<Item?> FindByIdAsync (Company company, int itemId, CancellationToken cancellationToken = default) =>
		await context!.Items!.Where(p => p.Id == itemId && p.CompanyId == company.Id)
			.Include(p => p.Inventory)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<TransactionResult> DeleteItemAsync (Item item)
	{
		try
		{
			var adjustments = await FindAdjustmentsByItemAsync(item);
			if (adjustments.Count > 0)
				context!.RemoveRange(adjustments);
			context!.RemoveRange(item);
			await context!.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteItemAsync), logger));
		}
	}


	/********************************************************************
	 * ITEM ADJUSTMENTS OPERATIONS
	 *******************************************************************/

	public async Task<IList<ItemAdjustment>> FindAdjustmentsByItemAsync (Item item, CancellationToken cancellationToken = default)
		=> await context!.ItemAdjustments!
			.Where(p => p.CompanyId == item.CompanyId && p.ItemId == item.Id)
			.Include(p => p.Item)
			.ToListAsync(cancellationToken);
	public async Task<IList<ItemAdjustment>> FindAdjustmentsAsync (Company company, CancellationToken cancellationToken = default)
		=> await context!.ItemAdjustments!
			.Where(p => p.CompanyId == company.Id)
			.Include(p => p.Item)
			.ToListAsync(cancellationToken);

	public async Task<ItemAdjustment?> FindAdjustmentByIdAsync (Company company, int adjustmentId, CancellationToken cancellationToken = default)
		=> await context!.ItemAdjustments!
			.Where(p => p.CompanyId == company.Id && p.Id == adjustmentId)
			.Include(p => p.Item)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<TransactionResult<ItemAdjustment>> CreateAdjustmentAsync (Item item, ItemAdjustment adjustment)
	{
		try
		{
			adjustment.CompanyId = item.CompanyId;
			adjustment.Id = item.Id;
			var result = await context!.ItemAdjustments!.AddAsync(adjustment);
			await context!.SaveChangesAsync();
			return TransactionResult<ItemAdjustment>.Success(result.Entity);

		}
		catch (Exception ex)
		{
			return TransactionResult<ItemAdjustment>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(CreateAdjustmentAsync), logger));
		}
	}

	public async Task<TransactionResult<ItemAdjustment>> UpdateAdjustmentAsync (ItemAdjustment adjustment)
	{
		try
		{
			var update = context!.ItemAdjustments!.Update(adjustment);
			await context!.SaveChangesAsync();
			return TransactionResult<ItemAdjustment>.Success(update.Entity);

		}
		catch (Exception ex)
		{
			return TransactionResult<ItemAdjustment>
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(UpdateAdjustmentAsync), logger));
		}
	}

	public async Task<TransactionResult> DeleteAdjustmentAsync (ItemAdjustment adjustment)
	{
		try
		{
			context!.ItemAdjustments!.RemoveRange(adjustment);
			await context!.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult
				.Failure(AppErrorDescriber.GetErrorFromDbException(ex, nameof(DeleteAdjustmentAsync), logger));
		}
	}


	/********************************************************************
	 * ITEM CATEGORIES
	 *******************************************************************/
	public Task<IList<ItemCategory>> FindCategoriesAsync (Company company, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<ItemCategory?> FindCategoryByIdAsync (Company company, int categoryId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<TransactionResult<ItemCategory>> CreateCategoryAsync (Company company, ItemCategory category)
	{
		throw new NotImplementedException();
	}

	public Task<TransactionResult<ItemCategory>> UpdateCategoryAsync (ItemCategory category)
	{
		throw new NotImplementedException();
	}

	public Task<TransactionResult> DeleteCategoryAsync (ItemCategory category)
	{
		throw new NotImplementedException();
	}
}
