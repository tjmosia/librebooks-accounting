using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseInvoicePayment))]
public class PurchaseInvoicePayment () : VersionedEntityBase()
{
	public virtual int PaymentId { get; set; }
	public virtual int InvoiceId { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Amount { get; set; }

	[MaxLength(255)]
	public virtual string? Comment { get; set; }

	public virtual PurchasePayment? Payment { get; set; }
	public virtual PurchaseDocument? Invoice { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<PurchaseInvoicePayment>(options =>	
		{
			options.HasKey(x => new { x.PaymentId, x.InvoiceId })
				.IsClustered();
		});
	}
}
