using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librebooks.Models.Entity.SalesSpace
{
    [Table(nameof(SalesQuoteProForma))]
    public class SalesQuoteProForma
    {
        public virtual int QuoteId { get; set;  }
        public virtual int ProFormId { get; set; }

        public SalesQuote? Quote { get; set; }
        public SalesProForma? ProForma { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<SalesQuoteProForma>(options =>
            {
                options.HasKey(p => new { p.QuoteId, p.ProFormId })
                    .IsClustered();
            });
        }
    }
}
