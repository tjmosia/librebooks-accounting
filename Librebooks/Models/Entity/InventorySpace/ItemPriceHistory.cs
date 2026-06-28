using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemPriceHistory))]
public class ItemPriceHistory () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }
	public virtual DateTime Date { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal OldPrice { get; set; }

    [Column(TypeName = ColumnTypes.MONETARY)]
    public virtual decimal NewPrice { get; set; }

	public virtual int ItemId { get; set; }
	public virtual int CompanyId { get; set; }
	public virtual Item? Item { get; set; }

	public static void OnModelCreating (ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ItemPriceHistory>(options =>
		{
			options.HasIndex(p => new { p.CompanyId, p.ItemId, p.Id })
				.IsClustered();
		});
	}
}
