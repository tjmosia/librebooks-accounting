using Librebooks.Core.EFCore;
using Librebooks.Core.Operations;
using Librebooks.Core.Util;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores
{
	public class PaymentMethodStore (AppDbContext context, ILogger<PaymentMethodStore> logger) : DbStoreBase(context, logger)
	{
		public async Task<PaymentMethod?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!.PaymentMethods!.FindAsync([id], cancellationToken);

		public async Task<IList<PaymentMethod>> FindAllAsync (CancellationToken cancellationToken)
			=> await context!.PaymentMethods!.ToListAsync(cancellationToken);

		public async Task<TransactionResult<PaymentMethod>> CreateAsync (PaymentMethod method, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await context!.PaymentMethods!.AddAsync(method, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult<PaymentMethod>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];

				if (DbExceptionUtils.IsUniqueKeyConstaint(ex))
					errors.Add(TransactionError.Create(nameof(PaymentMethod.Name), "Name is alreay in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult<PaymentMethod>.Failure([.. errors]);
			}
		}

		public async Task<TransactionResult<PaymentMethod>> UpdateAsync (PaymentMethod method, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = context!.PaymentMethods!.Update(method);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult<PaymentMethod>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];

				if (IsUniqueKeyConstaint(ex))
					errors.Add(TransactionError.Create(nameof(PaymentMethod.Name), "Name is alreay in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult<PaymentMethod>.Failure([.. errors]);
			}
		}

		public async Task<TransactionResult> DeleteAsync (PaymentMethod[] methods, CancellationToken cancellationToken = default)
		{
			try
			{
				context!.PaymentMethods!.RemoveRange(methods);
				await context.SaveChangesAsync(cancellationToken);
				return TransactionResult.Success;
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];

				if (IsForeignKeyViolation(ex))
					errors.Add(TransactionError.Create("", methods.Length > 1 ? "Unable to delete methods that are currently in use." : "Method is currently in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return TransactionResult.Failure([.. errors]);
			}
		}
	}
}
