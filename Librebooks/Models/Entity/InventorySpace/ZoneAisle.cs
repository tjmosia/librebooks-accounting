using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ZoneAisle))]
public class ZoneAisle
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id   { get; set; }

    [Required, MaxLength(75)]
    public virtual string? Name { get; set; }

    [MaxLength(155)]
    public virtual string? Description  { get; set; }

    public virtual int ZoneId { get; set; }

    public ICollection<AisleColumn>? Columns { get; set; }

    public virtual WarehouseZone? WarehouseZone { get; set; }

}
