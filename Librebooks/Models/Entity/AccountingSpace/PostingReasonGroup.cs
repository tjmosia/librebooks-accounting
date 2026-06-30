using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(PostingReasonGroup))]
public class PostingReasonGroup
{
	public virtual int Id { get; set; }
	public virtual string? Name { get; set; }

	public ICollection<PostingReason>? Reasons { get; set; }
}
