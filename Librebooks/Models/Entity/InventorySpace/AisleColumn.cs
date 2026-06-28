using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(AisleColumn))]
public class AisleColumn
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    public virtual int AisleId { get; set; }
    public ZoneAisle? Aisle { get; set; }

    public ICollection<ColumnShelve>? Shelves { get; set; }
}