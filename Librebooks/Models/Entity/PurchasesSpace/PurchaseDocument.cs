using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseDocument))]
public class PurchaseDocument () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }
    public virtual int StatusId { get; set; }
    public virtual int TypeId { get; set; }
    public virtual DateOnly Date { get; set; }
	public virtual DateOnly? DueDate { get; set; }

	[MaxLength(75)]
	public virtual string? Title { get; set; }

	[MaxLength(50)]
	public virtual string? Number { get; set; }

	[MaxLength(50)]
	public virtual string? SuppplierReference { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal SubTotalAmount { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal TaxAmount { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal TotalAmount { get; set; }

	public virtual bool? Recorded { get; set;  }

	[MaxLength(255)]
	public virtual string? Message { get; set; }

	[MaxLength(500)]
	public virtual string? FooterComment { get; set; }

	public virtual bool Printed { get; set; }
	public virtual int SupplierId { get; set;  }
	public virtual int? SupplierInfoId { get; set; }
	public virtual int CompanyInfoId { get; set; }
	public virtual int CompanyId { get; set; }

	public DocumentStatus? Status { get; set; }
	public ICollection<PurchaseDocumentLine>? Lines { get; set; }
	public DocumentSupplierDetail? SupplierInfo { get; set; }
	public DocumentCompanyDetails? CompanyInfo { get; set; }
	public DocumentType? Type { get; set; }
	public Company? Company { get; set; }
	public Supplier? Supplier { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<PurchaseDocument>(options =>
		{
			options.HasMany(p => p.Lines)
				.WithOne(p => p.Document)
				.HasForeignKey(p => p.DocumentId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Status)
				.WithMany()
				.HasForeignKey(p => p.StatusId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.SupplierInfo)
				.WithMany()
				.HasForeignKey(p => p.SupplierInfoId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Type)
				.WithMany()
				.HasForeignKey(p => p.TypeId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p=>p.Supplier)
				.WithMany()
				.HasForeignKey(p => p.SupplierId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p=>p.Company)
				.WithMany()
				.HasForeignKey(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

		});
	}
}
