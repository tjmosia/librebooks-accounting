using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesInvoiceCredit))]
[PrimaryKey(nameof(InvoiceId), nameof(CreditId))]
public class SalesInvoiceCredit
{
	public virtual int InvoiceId { get; set; }
	public virtual int CreditId { get; set; }

	public SalesCredit? Credit { get; set; }
	public SalesInvoice? Invoice { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<SalesInvoiceCredit>(options =>
		{
			options.ToTable(nameof(SalesInvoiceCredit))
				.HasKey(x => new { x.InvoiceId, x.CreditId });
		});
	}
}
