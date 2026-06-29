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
	public virtual string? ItemCode { get; set; }
	public virtual int CreditAccountId { get; set; }
	public virtual string? Description { get; set; }
	public virtual string? Unit { get; set; }
	public virtual decimal Quantity { get; set; }
	public virtual decimal Price { get; set; }
	public virtual decimal DiscountRate { get; set; }
	public virtual decimal TaxRate { get; set; }
	public virtual decimal NetPrice { get; set; }
	public virtual decimal LineSubTotal { get; set; }

	public virtual int TaxId { get; set; }
	public virtual int DocumentId { get; set; }
	public virtual string? Comment { get; set; }

	public ICollection<SalesDocumentLine>? DocumentLines { get; set; }
	public Item? Item { get; set; }
	public CompanyTax? Tax { get; set; }
	public SalesDocument? Document { get; set; }
	public LedgerAccount? CreditAccount { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<SalesDocumentLine>(options =>
		{

			options.HasKey(p => p.Id);
			options.Property(p => p.Id).UseIdentityColumn();
			options.Property(p => p.ItemCode).HasMaxLength(50);
			options.Property(p => p.Unit).HasMaxLength(50);
			options.Property(p => p.Quantity).HasColumnType(ColumnTypes.NUMBER);
			options.Property(p => p.Price).HasColumnType(ColumnTypes.MONETARY);
			options.Property(p => p.DiscountRate).HasColumnType(ColumnTypes.PERCENTAGE);
			options.Property(p => p.TaxRate).HasColumnType(ColumnTypes.PERCENTAGE);
			options.Property(p => p.NetPrice).HasColumnType(ColumnTypes.MONETARY);
			options.Property(p => p.LineSubTotal).HasColumnType(ColumnTypes.MONETARY);
			options.Property(p => p.Description).HasMaxLength(155);
			options.Property(p => p.Comment).HasMaxLength(255);

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
