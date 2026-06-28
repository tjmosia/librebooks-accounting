using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(WarehouseZone))]
public class WarehouseZone(): VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Code { get; set;  }

    [Required, MaxLength(155)]
    public virtual string? Name { get; set;  }

    public virtual int WarehouseId { get; set; }
    public ICollection<ZoneAisle> Aisles { get; set; }
    public Warehouse? Warehouse { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warehouse>(entity =>
        {
            
        });
    }
}
