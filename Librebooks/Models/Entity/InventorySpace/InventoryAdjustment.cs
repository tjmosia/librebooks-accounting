using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(InventoryAdjustment))]
public class InventoryAdjustment () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }
	public virtual DateTime Date { get; set; }

	[Required, MaxLength(50)]
	public virtual string? Reason { get; set; }

	public virtual int InventoryId { get; set; }
	public virtual int ItemId { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal OldQuantityOnHand { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal QuantityOnHand { get; set; }

	public virtual int CompanyId { get; set; }

	public virtual bool FromSales { get; set; }

	public virtual Item? Item { get; set; }
	public virtual Company? Company { get; set; }
    public virtual Inventory? Inventory { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<InventoryAdjustment>(options =>
		{
			options.HasIndex(p => new { p.CompanyId, p.ItemId, p.Id })
				.IsClustered();

			options.HasOne(p => p.Company)
				.WithMany()
				.HasForeignKey(p => p.CompanyId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
