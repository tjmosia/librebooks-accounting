using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(GoodIssue))]
public class GoodIssue
{
	public virtual int Id { get; set; }
	public virtual DateTime DateCreated { get; set;  }
	public virtual string? Number { get; set; }
	public ICollection<>



	public static void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<GoodIssue>( options =>
		{
			options.HasKey( x => x.Id );
			options.Property(x => x.Id).UseIdentityColumn();
		});
	}
}
