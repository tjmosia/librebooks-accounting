using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.SalesSpace;

namespace Librebooks.Areas.Customers.Providers;

public interface ISalesStore
{
    /**************************************************************************
     * SALES QUOTES
     *************************************************************************/
    Task<TransactionResult> CreateQuoteAsync(Company company, Customer customer, SalesDocument quoteDocument);
    Task<TransactionResult> UpdateQuoteAsync(SalesDocument quoteDocument);
    Task<SalesDocument> FindQuoteByIdASync(Company company, int documentId, CancellationToken cancellationToken = default);
    Task<TransactionResult> GetQuotesAsync(Company company, Customer customer, CancellationToken cancellationToken = default);
    Task<TransactionResult> GetQuotesAsync(Company company, CancellationToken cancellationToken = default);
    Task<SalesDocument> FindQuoteByNumberAsync(Company company, string number, CancellationToken cancellationToken = default);
    Task<TransactionResult> DeleteQuoteAsync(SalesQuote quote);

}
