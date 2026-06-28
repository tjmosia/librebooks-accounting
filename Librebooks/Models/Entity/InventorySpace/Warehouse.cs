using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(Warehouse))]
public class Warehouse
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(155)]
    public virtual string? Name { get; set; }

    public virtual int CompanyId { get; set; }
    public Company? Company { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasIndex(e => new { e.CompanyId, e.Id })
                .IsUnique()
                .IsClustered();

            entity.HasMany<ItemInventory>()
                .WithOne(p => p.Warehouse)
                .HasForeignKey(p => p.WarehouseId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

