
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(WarehouseBin))]
public class WarehouseBin() : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(20)]
    public virtual string? Code { get; set; }

    [MaxLength(255)]
    public virtual string? Name { get; set; }

    public virtual int ShelveId { get; set; }
    public virtual int CompanyId { get; set; }

    public WarehouseShelve? Shelve { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WarehouseBin>(entity =>
        {
            entity.HasIndex(p => new { p.CompanyId, p.ShelveId, p.Id })
                .IsClustered()
                .IsUnique();

            entity.HasOne<Company>()
                .WithOne()
                .HasForeignKey<WarehouseBin>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}