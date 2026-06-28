using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(JournalLine))]
public class JournalLine () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Debit{ get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Credit{ get; set; }

	public virtual int JournalId { get; set; }
	public virtual int CompanyId { get; set; }
	public virtual int LedgerAccountId { get; set; }
	public virtual Company? Company { get; set; }
	public virtual Journal? Journal { get; set; }
	public virtual CompanyLedgerAccount? LedgerAccount { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<JournalLine>(options =>
		{
			options.HasIndex(p => new { p.CompanyId, p.LedgerAccountId, p.JournalId, p.Id })
				.IsClustered();

			options.HasOne(p => p.Company)
				.WithMany()
				.HasForeignKey(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.LedgerAccount)
				.WithMany(p => p.JournalLines)
				.HasForeignKey(p => p.LedgerAccountId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
