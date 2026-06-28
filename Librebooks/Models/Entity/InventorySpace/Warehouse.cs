using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(Warehouse))]
public class Warehouse (): VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(20)]
    public virtual string? Code { get; set; }

    [Required, MaxLength(155)]
    public virtual string? Name { get; set; }

    public virtual int CompanyId { get; set; }

    public Company? Company { get; set; }
    public ICollection<Inventory>? Inventories { get; set; }
    public ICollection<WarehouseZone>? Zones { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasIndex(e => new { e.CompanyId, e.Id })
                .IsUnique()
                .IsClustered();

            entity.HasMany(p=>p.Inventories)
                .WithOne(p => p.Warehouse)
                .HasForeignKey(p => p.WarehouseId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p=>p.Zones)
                .WithOne(p => p.Warehouse)
                .HasForeignKey(p => p.WarehouseId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Company>()
                .WithOne()
                .HasForeignKey<Warehouse>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

