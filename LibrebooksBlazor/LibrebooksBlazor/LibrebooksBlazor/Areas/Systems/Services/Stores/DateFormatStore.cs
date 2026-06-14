using LibrebooksBlazor.Core.EFCore;
using LibrebooksBlazor.Core.Operations;
using LibrebooksBlazor.Data;
using LibrebooksBlazor.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Areas.Systems.Services.Stores
{
	public class DateFormatStore (AppDbContext context, ILogger<DateFormatStore> logger) : DbStoreBase(context, logger)
	{
		public async Task<TransactionResult<DateFormat>> CreateAsync (DateFormat dateFormat, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await context!.DateFormats!.AddAsync(dateFormat, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult<DateFormat>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];
				if (IsUniqueKeyConstaint(ex))
					errors.Add(TransactionError.Create(nameof(DateFormat.Format), "Format is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult<DateFormat>.Failure([.. errors]);
			}
		}

		public async Task<TransactionResult<DateFormat>> UpdateAsync (DateFormat dateFormat, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = context!.DateFormats!.Update(dateFormat);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult<DateFormat>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];
				if (IsUniqueKeyConstaint(ex))
					errors.Add(TransactionError.Create(nameof(DateFormat.Format), "Format is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult<DateFormat>.Failure([.. errors]);
			}
		}

		public async Task<DateFormat?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!.DateFormats!.FindAsync([id], cancellationToken);

		public async Task<TransactionResult> DeleteAsync (DateFormat[] dateFormats, CancellationToken cancellationToken = default)
		{
			try
			{
				context!.DateFormats!.RemoveRange(dateFormats);
				var result = await context.SaveChangesAsync(cancellationToken);
				return TransactionResult.Success;
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];

				if (IsForeignKeyViolation(ex))
					errors.Add(TransactionError.Create("", dateFormats.Length > 1 ? "One or more date formats are currently in use." : "The date format is curently in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult.Failure([.. errors]);
			}
		}

		public async Task<IList<DateFormat>> FindAllAsync (CancellationToken cancellationToken = default)
			=> await context!.DateFormats!.ToListAsync(cancellationToken);
	}
}
