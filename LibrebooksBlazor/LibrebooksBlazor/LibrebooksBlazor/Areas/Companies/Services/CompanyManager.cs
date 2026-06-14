namespace LibrebooksBlazor.Areas.Companies.Services;

public class CompanyManager (ICompanyStore store) : ICompanyManager
{
	private readonly ICompanyStore store = store;

}
