using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(PostingReason))]
public class PostingReason
{
	public virtual int Id	{ get; set; }
	public virtual int GroupId { get; set; }
	public virtual string? Name { get; set; }
	public PostingRule? 
}
