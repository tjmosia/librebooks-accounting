using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(WarehouseRow))]
public class WarehouseRow() : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id   { get; set; }

    [Required, MaxLength(20)]
    public virtual string? Code { get; set; }

    [MaxLength(255)]
    public virtual string? Name  { get; set; }

    public virtual int ZoneId { get; set; }
    public virtual int CompanyId { get; set;  }

    public virtual WarehouseZone? Zone { get; set; }
    public ICollection<WarehouseColumn>? Columns { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WarehouseRow>(entity =>
        {
            entity.HasIndex(p => new { p.CompanyId, p.ZoneId, p.Id })
                .IsClustered()
                .IsUnique();

            entity.HasMany(p => p.Columns)
                .WithOne(p => p.Row)
                .HasForeignKey(p => p.RowId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Company>()
                .WithOne()
                .HasForeignKey<WarehouseRow>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
