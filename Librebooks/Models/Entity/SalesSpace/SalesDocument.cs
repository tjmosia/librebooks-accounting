using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.IdentitySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocument))]
public class SalesDocument () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }
    public virtual int TypeId { get; set; }
    public virtual int StatusId { get; set; }
    public virtual DateOnly Date { get; set; }
	public virtual DateOnly DueDate { get; set; }

	[Required, MaxLength(50)]
	public virtual string? Title { get; set; }

	[MaxLength(20)]
	public virtual string? Number { get; set; }

	[MaxLength(50)]
	public virtual string? CustomerReference { get; set; }

	[MaxLength(255)]
	public virtual string? Message { get; set; }

	[MaxLength(500)]
	public virtual string? FooterComment { get; set; }

	public virtual int CurrencyId { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal SubTotalAmount { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal TaxAmount { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal TotalAmount { get; set; }

	public virtual int CustomerId { get; set; }
	public virtual int CompanyId { get; set; }
	public virtual int CustomerInfoId { get; set; }
	public virtual int CompanyInfoId { get; set; }
	public string? SalesPersonId { get; set; }
	public virtual bool Recorded { get; set; }
	public virtual bool Printed { get; set; }
	public int? CreatorId { get; set; }

    public DocumentStatus? Status { get; set; }
	public SalesPerson? SalesPerson { get; set; }
	public SalesDocumentCustomerDetails? CustomerInfo { get; set; }
	public DocumentCompanyDetails? CompanyInfo { get; set; }
	public ICollection<SalesDocumentLine>? Lines { get; set; }
	public Company? Company { get; set; }
	public Customer? Customer { get; set; }
	public User? Creator { get; set; }
	public DocumentType? Type { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<SalesDocument>(options =>
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

			options.HasOne(p => p.Customer)
				.WithMany()
				.HasForeignKey(p => p.CustomerId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Company)
				.WithMany()
				.HasForeignKey(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
