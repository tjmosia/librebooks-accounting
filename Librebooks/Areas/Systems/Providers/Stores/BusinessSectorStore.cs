using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Core.Util;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;
namespace Librebooks.Areas.Systems.Providers.Stores
{
	public class BusinessSectorStore (AppDbContext context, ILogger<CountryStore> logger) : DbStoreBase(context, logger)
	{
		public async Task<IList<BusinessSector>> FindAllAsync (CancellationToken cancellationToken = default)
			=> await context!
				.BusinessSectors!
				.OrderBy(p => p.Name)
				.ToListAsync(cancellationToken);

		public async Task<BusinessSector?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!
				.BusinessSectors!
				.FindAsync([id], cancellationToken);

		public async Task<IList<BusinessSector>> FindByIdsAsync (int[] sectorIds, CancellationToken cancellationToken = default)
			=> await context!
				.BusinessSectors!
				.Where(p => sectorIds.Contains(p.Id))
				.ToListAsync(cancellationToken);

		public async Task<BusinessSector?> FindByNameAsync (string name, CancellationToken cancellationToken = default)
			=> await context.BusinessSectors!.Where(p => p.Name == name)
				.FirstOrDefaultAsync(cancellationToken);

		public async Task<TransactionResult<BusinessSector>> CreateAsync (BusinessSector sector, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await context!.BusinessSectors!.AddAsync(sector, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult<BusinessSector>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];
				if (IsUniqueKeyConstaint(ex))
					errors.Add(new(nameof(BusinessSector.Name), "Name is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult<BusinessSector>.Failure([.. errors]);
			}
		}

		public async Task<TransactionResult<BusinessSector>> UpdateAsync (BusinessSector sector, CancellationToken cancellationToken = default)
		{
			try
			{
				sector.RefreshConcurrencyToken();
				var result = context!.BusinessSectors!.Update(sector);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult<BusinessSector>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];
				if (IsUniqueKeyConstaint(ex))
					errors.Add(new(nameof(BusinessSector.Name), "Name is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult<BusinessSector>.Failure([.. errors]);
			}
		}

		public async Task<TransactionResult> DeleteAsync (BusinessSector[] sectors, CancellationToken cancellationToken = default)
		{
			try
			{
				context!.BusinessSectors!.RemoveRange(sectors);
				await context.SaveChangesAsync(cancellationToken);

				return TransactionResult.Success;
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];

				if (DbExceptionUtils.IsForeignKeyViolation(ex))
					errors.Add(new(description: sectors.Length > 1 ? "One of more business sectors are currently in use." : "Business sector is currently in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult.Failure([.. errors]);
			}
		}
	}
}
