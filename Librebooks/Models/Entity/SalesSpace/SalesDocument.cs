using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocument))]
public class SalesDocument () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }
	public virtual DateOnly Date { get; set; }
	public virtual DateOnly DueDate { get; set; }

	[Required, MaxLength(50)]
	public virtual string? Title { get; set; }

	[MaxLength(20)]
	public virtual string? Number { get; set; }

	[MaxLength(50)]
	public virtual string? CustomerReference { get; set; }

	public virtual int CustomerInfoId { get; set; }
	public virtual int CompanyInfoId { get; set; }

	[MaxLength(255)]
	public virtual string? Message { get; set; }

	[MaxLength(500)]
	public virtual string? FooterMessage { get; set; }

	public virtual bool TaxExempt { get; set; }
	public virtual int CurrencyId { get; set; }
	public string? SalesPersonId { get; set; }
	public virtual int? ShippingTermId { get; set; }
	public virtual int? ShippingMethodId { get; set; }
	public virtual int CustomerId { get; set; }
	public virtual bool Recorded { get; set; }
	public virtual int StatusId { get; set; }
	public virtual bool Printed { get; set; }
	public int? CreatorId { get; set; }
	public virtual int TypeId { get; set; }

	public DocumentType? Type { get; set; }
	public Currency? Currency { get; set; }
	public DocumentStatus? Status { get; set; }
	public SalesPerson? SalesPerson { get; set; }
	public ShippingMethod? ShippingMethod { get; set; }
	public ShippingTerm? ShippingTerm { get; set; }
	public DocumentCustomerDetails? CustomerInfo { get; set; }
	public DocumentCompanyDetail? CompanyInfo { get; set; }
	public ICollection<SalesDocumentNote>? Notes { get; set; }
	public ICollection<SalesDocumentLine>? Lines { get; set; }
	public User? Creator { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
		=> builder.Entity<SalesDocument>(options =>
		{
			options.HasMany(p => p.Lines)
				.WithOne(p => p.Document)
				.HasForeignKey(p => p.DocumentId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			options.HasOne(p => p.Creator)
				.WithOne()
				.HasForeignKey<SalesDocument>(p => p.CreatorId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			options.HasMany(p => p.Notes)
				.WithOne()
				.HasForeignKey(p => p.DocumentId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			options.HasOne(p => p.Status)
				.WithMany()
				.HasForeignKey(p => p.StatusId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Type)
				.WithMany()
				.HasForeignKey(p => p.TypeId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
}
