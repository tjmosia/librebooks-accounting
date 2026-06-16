using Librebooks.Core.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Systems.Providers
{
	public interface ISystemsStore
	{
		/*****************************************************************
		 * COMPANY SETUP OPERATIONS
		 *****************************************************************/
		Task<CompanySetup?> FindCurrentCompanySetupAsync (CancellationToken cancellationToken = default);
		Task<TransactionResult<CompanySetup>> CreateCompanySetupAsync (CompanySetup setup, CancellationToken cancellationToken = default);
		Task<TransactionResult<CompanySetup>> UpdateCompanySetupAsync (CompanySetup setup, CancellationToken cancellationToken = default);

		/*****************************************************************
		 * TAX OPERATIONS
		 *****************************************************************/
		Task<TransactionResult<Tax>> CreateTaxAsync (Tax vat, CancellationToken cancellationToken = default);
		Task<TransactionResult<Tax>> UpdateTaxAsync (Tax vat, CancellationToken cancellationToken = default);
		Task<Tax?> FindTaxByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteTaxesAsync (Tax[] taxes, CancellationToken cancellationToken = default);
		Task<IList<Tax>> FindTaxesAsync (CancellationToken cancellationToken = default);

		/*****************************************************************
		 * COUNTRY OPERATIONS
		 *****************************************************************/
		Task<Country?> FindByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<IList<Country>> FindCountriesAsync (CancellationToken cancellationToken = default);
		Task<IList<Country>> FindCountryByIdsAsync (int[] ids, CancellationToken cancellationToken = default);
		Task<TransactionResult<Country>> CreateAsync (Country country, CancellationToken cancellationToken = default);
		Task<TransactionResult<Country>> UpdateCountryAsync (Country country, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteCountriesAsync (params Country[] countries);

		/*****************************************************************
		 * CURRENCY OPERATIONS
		 *****************************************************************/
		Task<IList<Currency>> FindCurrenciesAsync (CancellationToken cancellationToken = default);
		Task<Currency?> FindCurrencyByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<TransactionResult<Currency>> CreateCurrencyAsync (Currency currency, CancellationToken cancellationToken = default);
		Task<TransactionResult<Currency>> UpdateCurrencyAsync (Currency currency, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteCurrenciesAsync (params Currency[] currencies);
		Task<Currency?> FindDefaultCurrencyAsync (CancellationToken cancellationToken = default);

		/*****************************************************************
		 * BUSINESS SECTOR OPERATIONS
		 *****************************************************************/
		Task<IList<BusinessSector>> FindBusinessSectorsAsync (CancellationToken cancellationToken = default);
		Task<BusinessSector?> FindBusinessSectorByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<IList<BusinessSector>> FindBusinessSectorsByIdsAsync (int[] sectorIds, CancellationToken cancellationToken = default);
		Task<TransactionResult<BusinessSector>> CreateAsync (BusinessSector sector, CancellationToken cancellationToken = default);
		Task<TransactionResult<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector sector, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteBusinessSectorsAsync (params BusinessSector[] sectors);

		/*****************************************************************
		 * DATE FORMATS OPERATIONS
		 *****************************************************************/
		Task<TransactionResult<DateFormat>> CreateDateFormatAsync (DateFormat dateFormat, CancellationToken cancellationToken = default);
		Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat, CancellationToken cancellationToken = default);
		Task<DateFormat?> FindDateFormatByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteDateFormatsAsync (params DateFormat[] dateFormats);
		Task<IList<DateFormat>> FindDateFormatsAsync (CancellationToken cancellationToken = default);
		Task<DateFormat?> FindDefaultDateFormatAsync (CancellationToken cancellationToken = default);

		/*****************************************************************
		 * SHIPPING TERMS OPERATIONS
		 *****************************************************************/
		Task<IList<ShippingTerm>> FindShippingTermsAsync (CancellationToken cancellationToken = default);
		Task<TransactionResult<ShippingTerm>> CreateShippingTermAsync (ShippingTerm term, CancellationToken cancellationToken = default);
		Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm term, CancellationToken cancellationToken = default);
		Task<ShippingTerm?> FindShippingTermByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteShippingTermsAsync (params ShippingTerm[] terms);

		/*****************************************************************
		 * SHIPPING METHOD OPERATIONS
		 *****************************************************************/
		Task<TransactionResult<ShippingMethod>> CreateShippingMethodAsync (ShippingMethod method, CancellationToken cancellationToken = default);
		Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod method, CancellationToken cancellationToken = default);
		Task<ShippingMethod?> FindShippingMethodByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeleteShippingMethodsAsync (params ShippingMethod[] methods);
		Task<IList<ShippingMethod>> FindShippingMethodsAsync (CancellationToken cancellationToken = default);

		/*****************************************************************
		 * PAYMENT METHOD OPERATIONS
		 *****************************************************************/
		Task<PaymentMethod?> FindPaymentMethodByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<IList<PaymentMethod>> FindPaymentMethodsAsync (CancellationToken cancellationToken);
		Task<TransactionResult<PaymentMethod>> CreatePaymentMethodAsync (PaymentMethod method, CancellationToken cancellationToken = default);
		Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod method, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeletePaymentMethodsAsync (params PaymentMethod[] methods);

		/*****************************************************************
		 * PAYMENT TERM OPERATIONS
		 *****************************************************************/
		Task<TransactionResult<PaymentTerm>> CreatePaymentTermAsync (PaymentTerm term, CancellationToken cancellationToken = default);
		Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm term, CancellationToken cancellationToken = default);
		Task<PaymentTerm?> FindPaymentTermByIdAsync (int id, CancellationToken cancellationToken = default);
		Task<TransactionResult> DeletePaymentTermsAsync (params PaymentTerm[] terms);
		Task<IList<PaymentTerm>> FindPaymentTermsAsync (CancellationToken cancellationToken = default);
	}
}
