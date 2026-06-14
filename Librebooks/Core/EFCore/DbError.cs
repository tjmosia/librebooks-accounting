using Librebooks.Core.Operations;

namespace Librebooks.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
