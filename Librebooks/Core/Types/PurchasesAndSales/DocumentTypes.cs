namespace Librebooks.Core.Types.PurchasesAndSales;

public readonly struct DocumentTypes
{
	public static readonly (string Name, int Value)
		/***************************************************
		 * CUSTOMERS DOCUMENTS
		 **************************************************/
		CustomerQuote = (Name: "Quote", Value: 1),
		CustomerSalesOrder = (Name: "Sales Order", Value: 2),
		CustomerProForma = (Name: "Pro Forma", Value: 3),
		CustomerInvoice = (Name: "Tax Invoice", Value: 4),
		CustomerCreditNote = (Name: "Credit Note", Value: 5),
		CustomerReceipt = (Name: "Customer Receipt", Value: 6),
		CustomerAdjustment = (Name: "Customer Adjustment", Value: 7),
		CustomerWriteOff = (Name: "Customer Write Off", Value: 8),

		/***************************************************
		 * SUPPLIER DOCUMENTS
		 **************************************************/
		SupplierQuote = (Name: "Request For Quote", Value: 9),
		SupplierPurchaseOrder = (Name: "Purchase Order", Value: 10),
		SupplierInvoice = (Name: "Supplier Invoice", Value: 11),
		SupplierReturn = (Name: "Supplier Return", Value: 12),
		SupplierPayment = (Name: "Supplier Payment", Value: 13),
		SupplierCreditNote = (Name: "Supplier Credit Note", Value: 14),
		SupplierAdjustment = (Name: "Supplier Adjustment", Value: 15);
}
