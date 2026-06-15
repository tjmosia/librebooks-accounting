using Librebooks.Models.Entity.CompanySpace;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesProForma))]
public class SalesProForma
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int DocumentId { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int CustomerId { get; set; }

    public Company? Company { get; set;  }
    public Document? Document { get; set; }

    public ICollection<SalesQuoteProForma>? AssociatedQuotes { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesProForma>(options =>
        {
            options.HasIndex(p => new { p.DocumentId, p.CompanyId })
                .IsClustered()
                .IsUnique(true);

            options.HasMany(p => p.AssociatedQuotes)
                .WithOne(p => p.ProForma)
                .HasForeignKey(p => p.ProFormId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
