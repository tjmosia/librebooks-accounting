using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Providers.Stores;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Librebooks.Areas.Inventory.Providers;

public sealed class ItemStore (AppDbContext context, ILogger<ItemStore> logger) : DbStoreBase(context), IItemStore
{
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
            return TransactionResult<Item>.Failure(() =>
            {
                if (IsUniqueConstaint(ex) && ex.InnerException!.Message.Contains($"{nameof(context.Items)}.{nameof(Item.Cost)}"))
                    return new TransactionError(nameof(AppErrorDescriber.UniqueKeyOrIndexConstraint), "Item code must be unique.");
                return GeneralError;
            });
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
			return TransactionResult<Item>.Failure( () =>
			{
				if (IsUniqueConstaint(ex) && ex.InnerException!.Message.Contains($"{nameof(context.Items)}.{nameof(Item.Cost)}"))
					return new TransactionError(nameof(AppErrorDescriber.UniqueKeyOrIndexConstraint), "Item code must be unique.");
				return GeneralError;
			});
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
    public async Task<IList<ItemCategory>> FindCategoriesAsync(Company company, CancellationToken cancellationToken = default) 
		=> await context.ItemCategories!.Where(p => p.CompanyId == company.Id)
				.ToListAsync(cancellationToken);

    public async Task<ItemCategory?> FindCategoryByIdAsync(Company company, int categoryId, CancellationToken cancellationToken = default)
		=> await context.ItemCategories!.Where(p => p.CompanyId == company.Id && p.Id == categoryId)
				.FirstOrDefaultAsync(cancellationToken);

    public async Task<TransactionResult<ItemCategory>> CreateCategoryAsync (Company company, ItemCategory category)
	{
		try
		{
			category.CompanyId = company.Id;
			var add = await context.ItemCategories!.AddAsync(category);
			await context.SaveChangesAsync();
			return TransactionResult<ItemCategory>.Success(add.Entity);
		}
		catch (Exception ex)
		{
			return TransactionResult<ItemCategory>.Failure(() =>
            {
                if (IsUniqueConstaint(ex) && ex.InnerException!.Message.Contains("Name"))
                    return new TransactionError(nameof(AppErrorDescriber.UniqueKeyOrIndexConstraint), "Category name must be unique.");
                return GeneralError;
			});
		}
	}

	public async Task<TransactionResult<ItemCategory>> UpdateCategoryAsync (ItemCategory category)
    {
        try
        {
            var update = context.ItemCategories!.Update(category);
            await context.SaveChangesAsync();
            return TransactionResult<ItemCategory>.Success(update.Entity);
        }
        catch (Exception ex)
        {
            return TransactionResult<ItemCategory>.Failure(() =>
            {
                if (IsUniqueConstaint(ex) && ex.InnerException!.Message.Contains("Name"))
                    return new TransactionError(nameof(AppErrorDescriber.UniqueKeyOrIndexConstraint), "Category name must be unique.");
                return GeneralError;
            });
        }
    }

	public async Task<TransactionResult> DeleteCategoryAsync (params ItemCategory[] categories)
	{
		try
		{
			context.ItemCategories!.RemoveRange(categories);
			await context.SaveChangesAsync();
			return TransactionResult.Success;
		}
		catch (Exception ex)
		{
			return TransactionResult.Failure(() =>
			{
				if (IsForeignKeyViolation(ex))
					return new TransactionError(nameof(AppErrorDescriber.ForeignKeyConstraint), categories.Length > 1 ? "One or more categories have items listed under them." : "Category has items listed under it.");
				return GeneralError;
			});
		}
	}
}
