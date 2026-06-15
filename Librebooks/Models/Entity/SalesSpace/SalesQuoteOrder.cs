using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesQuoteOrder))]
[PrimaryKey(nameof(QuoteId), nameof(OrderId))]
public class SalesQuoteOrder
{
	public virtual int QuoteId { get; set; }
	public virtual int OrderId { get; set; }

	public SalesQuote? Quote { get; set; }
	public SalesOrder? Order { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<SalesQuoteOrder>(options =>
		{
		});
	}
}
