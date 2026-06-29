using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerWriteOff))]
public class CustomerWriteOff : VersionedEntityBase
{
	public virtual int Id { get; set; }
	public virtual DateOnly Date { get; set; }
	public virtual string? Number { get; set; }
	public virtual string? CustomerName { get; set; }
	public virtual string? Reference { get; set; }
	public virtual decimal Amount { get; set; }
	public virtual decimal TaxRate { get; set; }
	public virtual decimal TaxAmount { get; set; }
	public virtual string? Description { get; set; }
	public virtual bool Posted { get; set; }
	public virtual int CompanyId { get; set; }
	public virtual int CustomerId { get; set; }
	public virtual int DebitLedgerAccountId { get; set; }
	public virtual int CreditLedgerAccountId { get; set; }

	public virtual ICollection<SalesInvoiceWriteoff>? Invoices { get; set; }
	public virtual Customer? Customer { get; set; }
	public virtual Company? Company { get; set; }
	public LedgerAccount? DebitLedgerAccount { get; set; }
	public LedgerAccount? CreditLedgerAccount { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<CustomerWriteOff>(options =>
		{
			// Key
			options.HasKey(x => x.Id);
			options.Property(x => x.Id).UseIdentityColumn();

			// Indexes
			options.HasIndex(p => new { p.CompanyId, p.CustomerId, p.Id })
				.IsClustered();

			options.HasIndex(p => new { p.CompanyId, p.Number })
				.IsUnique();

			// Properties
			options.Property(x => x.CustomerName)
				.HasMaxLength(100);

			options.Property(x => x.Number)
				.IsRequired()
				.HasMaxLength(50);

			options.Property(x => x.Reference)
				.HasMaxLength(50);

			options.Property(x => x.Amount)
				.HasColumnType(ColumnTypes.MONETARY);

			options.Property(x => x.TaxAmount)
				.HasColumnType(ColumnTypes.MONETARY);

			options.Property(x => x.TaxRate)
				.HasColumnType(ColumnTypes.PERCENTAGE);

			options.Property(x => x.Description)
				.HasMaxLength(255);

			// Relationships
			options.HasMany(p => p.Invoices)
				.WithOne(p => p.WriteOff)
				.HasForeignKey(p => p.WriteOffId).IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.DebitLedgerAccount)
				.WithOne()
				.HasForeignKey<CustomerWriteOff>(p => p.DebitLedgerAccountId).IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.CreditLedgerAccount)
				.WithOne()
				.HasForeignKey<CustomerWriteOff>(p => p.CreditLedgerAccountId).IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Company)
				.WithOne()
				.HasForeignKey<CustomerWriteOff>(p => p.CompanyId).IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Customer)
				.WithOne()
				.HasForeignKey<CustomerWriteOff>(p => p.CustomerId).IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
