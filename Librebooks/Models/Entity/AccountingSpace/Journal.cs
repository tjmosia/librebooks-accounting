using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(Journal))]
public class Journal
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[Column(ColumnTypes.DATE)]
	public virtual DateOnly DateCreated { get; set; }

	[MaxLength(50)]
	public virtual string? Reference { get; set; }

	[MaxLength(255)]
	public virtual string? Description { get; set; }
	public virtual int CompanyId { get; set; }

	public virtual bool Posted { get; set; }
	public virtual int? SourceId { get; set; }

	[Column(TypeName = "CHAR(1)")]
	public virtual string? SourceType { get; set; }

	public Company? Company { get; set; }

	public ICollection<JournalLine>? Entries { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<Journal>(options =>
		{
			options.HasIndex(p => new { p.CompanyId, p.Id })
				.IsClustered()
				.IsUnique();

			options.HasMany(p => p.Entries)
				.WithOne(p => p.Journal)
				.HasForeignKey(p => p.JournalId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
