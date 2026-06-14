using LibrebooksBlazor.Core.Operations;

namespace LibrebooksBlazor.Core.EFCore
{
	public class DbErrorDescriber
	{
		public DbError IndexConstraint (string? description = null)
			=> new()
			{
				ErrorNumber = DbEngineErrorsCodes.IndexConstraint,
				Error = new TransactionError(nameof(IndexConstraint), description ?? "")
			};

		public DbError PrimaryKeyConstraint (string? description = null)
			=> new()
			{
				ErrorNumber = DbEngineErrorsCodes.PrimaryKeyConstraint,
				Error = new TransactionError(nameof(PrimaryKeyConstraint), description ?? "")
			};
	}
}
