using LibrebooksBlazor.Client.Models;

namespace LibrebooksBlazor.Client.Providers.Services;

public interface IIdentityService
{
	UserSessionData? GetUser ();
	CompanySessionData? GetCompany ();

	void UpdateUser (UserSessionData userSessionData);

	void UpdateCompany (CompanySessionData companySessionData);

	void Clear ();
}
