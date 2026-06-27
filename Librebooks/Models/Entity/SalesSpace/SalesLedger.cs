using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesLedger))]
public class SalesLedger
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[MaxLength(50), Required]
	public virtual string? DocumentNumber { get; set; }

	[Column(TypeName = ColumnTypes.DATE)]
	public virtual DateOnly Date { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Amount { get; set; }

	public virtual int EntryTypeId { get; set; }
	public virtual int DocumentId { get; set; }
	public virtual int CustomerId { get; set; }
	public virtual int CompanyId { get; set; }

	public Customer? Customer { get; set; }
	public Company? Company { get; set; }
	public SalesLedgerEntryType? EntryType { get; set; }

	public static void OnModelCreating (ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SalesLedger>(entity =>
		{
			entity.HasIndex(p => new { p.CompanyId, p.CustomerId, p.Id })
				.IsClustered();

			entity.HasOne(p => p.EntryType)
				.WithMany()
				.HasForeignKey(p => p.EntryTypeId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(p => p.Customer)
				.WithMany()
				.HasForeignKey(p => p.CustomerId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(p => p.Company)
			.WithMany()
			.HasForeignKey(p => p.CompanyId)
				.IsRequired()
			.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
