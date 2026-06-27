namespace Librebooks.Core.Types.PurchasesAndSales;

public readonly struct DocumentTypes
{
	public static readonly (string Name, int Value) CustomerQuote = (Name: "Quote", Value: 1),
		CustomerSalesOrder = (Name: "Sales Order", Value: 2),
		CustomerProForma = (Name: "Pro Forma", Value: 3),
		CustomerInvoice = (Name: "Invoice", Value: 4),
		CustomerCreditNote = (Name: "Credit Note", Value: 5),
		CustomerReceipt = (Name: "Receipt", Value: 6),
		CustomerAdjustment = (Name: "Adjustment", Value: 7),
		CustomerWriteOff = (Name: "Write Off", Value: 8),
		// SUPPLIER DOCUMENTS
		SupplierQuote = (Name: "Purchase Order", Value: 9),
		SupplierPurchaseOrder = (Name: "Supplier Purchase Order", Value: 10),
		SupplierInvoice = (Name: "Supplier Invoice", Value: 11),
		SupplierReturn = (Name: "Supplier Return", Value: 12),
		SupplierPayment = (Name: "Supplier Payment", Value: 13);
}
