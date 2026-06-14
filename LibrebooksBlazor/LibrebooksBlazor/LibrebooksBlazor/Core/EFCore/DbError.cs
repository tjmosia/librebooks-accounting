using LibrebooksBlazor.Core.Operations;

namespace LibrebooksBlazor.Core.EFCore
{
	public class DbError
	{
		public int ErrorNumber { get; set; }
		public TransactionError? Error { get; set; }
	}
}
