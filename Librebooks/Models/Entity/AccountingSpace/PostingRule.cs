using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(PostingRule))]
public class PostingRule
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	public virtual int ReasonId { get; set; }
	public virtual int CreditAccountId { get; set; }
	public virtual int DebitAccountId { get; set; }

	public LedgerAccount? DebitAccount { get; set; }
	public LedgerAccount? CreditAccount { get; set;  }
	public PostingReason? Reason { get; set; }
}
