using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.AccountingSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesLedgerJournal))]
public class SalesLedgerJournal
{
	public virtual int JournalId { get; set; }
	public virtual int SalesLedgerId { get; set; }

	public Journal? Journal { get; set; }
	public SalesLedger? SalesLedger { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<SalesLedgerJournal>(options =>
		{
			options.HasKey(p => new { p.JournalId, p.SalesLedgerId });

			options.HasOne(p => p.Journal)
				.WithOne()
				.HasForeignKey<SalesLedgerJournal>(p => p.JournalId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.SalesLedger)
				.WithOne(p => p.Journal)
				.HasForeignKey<SalesLedgerJournal>(p => p.SalesLedgerId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
