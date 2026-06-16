using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemInventory))]
public class ItemInventory () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public virtual int ItemId { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal QuantityOnHand { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal MinQuantity { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal MaxQuantity { get; set; }

	public virtual Item? Item { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<ItemInventory>(options =>
		{
		});
	}
}
