using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ColumnShelve))]
public class ColumnShelve
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    public virtual int ColumnId { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Name { get; set; }

    [MaxLength(555)]
    public virtual string? Description { get; set;  }

    public AisleColumn? Column { get; set; }

    public ICollection<ShelveBin>? Bins { get; set; }

}