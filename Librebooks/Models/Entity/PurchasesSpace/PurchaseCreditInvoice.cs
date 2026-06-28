using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseCreditInvoice))]
public class PurchaseCreditInvoice
{
    public virtual int CreditId { get; set; }
    public virtual int InvoiceId { get; set; }

    public PurchaseDocument? Credit { get; set; }
    public PurchaseDocument? Invoice { get; set;  }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseCreditInvoice>(enttity =>
        {
            enttity.HasKey(x => new {x.CreditId, x.InvoiceId})
                .IsClustered();
        });
    }
}
