using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesLedgerSourceType))]
[Index(nameof(SalesLedgerSourceType), IsUnique = true)]
public class SalesLedgerSourceType(): VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(155)]
    public virtual string? Name { get; set; }

    [Required, MaxLength(5)]
    public virtual string? Code { get; set;  }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesLedger>(entity =>
        {
            entity.HasIndex(p => new { p.Id })
                .IsClustered();


        });
    }
}
