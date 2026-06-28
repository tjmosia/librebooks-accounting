using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocumentLine))]
public class SalesDocumentLine () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	public virtual int? ItemId { get; set; }

	[MaxLength(50)]
	public virtual string? ItemCode { get; set; }

	public virtual int DebitAccountId { get; set; }

	[MaxLength(255)]
	public virtual string? Description { get; set; }

	[MaxLength(20)]
	public virtual string? Unit { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal Quantity { get; set;  }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Price { get; set; }

	[Column(TypeName = ColumnTypes.PERCENTATE)]
	public virtual decimal DiscountRate { get; set; }

	[Column(TypeName = ColumnTypes.PERCENTATE)]
	public virtual decimal TaxRate { get; set; }

    [Column(TypeName = ColumnTypes.MONETARY)]
    public virtual decimal NetPrice { get; set; }

    [Column(TypeName = ColumnTypes.MONETARY)]
    public virtual decimal LineSubTotal { get; set; }

	public virtual int TaxId { get; set; }
	public virtual int DocumentId { get; set; }

	[MaxLength(500)]
	public virtual string? Comment { get; set; }

	public ICollection<SalesDocumentLine>? DocumentLines { get; set; }
	public Item? Item { get; set; }
	public CompanyTax? Tax { get; set; }
	public SalesDocument? Document { get; set; }
	public LedgerAccount? DebitAccount { get; set;  }
	public LedgerAccount? CreditAccount { get; set;  }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<SalesDocumentLine>(options =>
		{
			
			options.HasIndex(p => new { p.DocumentId, p.Id })
				.IsClustered()
				.IsUnique();

			options.HasOne<Item>()
				 .WithMany()
				 .HasPrincipalKey(i => i.Code)
				 .OnDelete(DeleteBehavior.Restrict);
		});
	}
}
