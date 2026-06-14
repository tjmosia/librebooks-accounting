using LibrebooksBlazor.Client.Models;

namespace LibrebooksBlazor.Client.Providers.Services;

public class IdentityService : IIdentityService
{
	private UserSessionData? User { get; set; }

	private CompanySessionData? Company { get; set; }

	public UserSessionData? GetUser ()
	{
		return User;
	}

	public CompanySessionData? GetCompany ()
	{
		throw new NotImplementedException();
	}

	public void UpdateUser (UserSessionData userSessionData)
	{
		User = userSessionData;
		NotifyChanged();
	}

	public void UpdateCompany (CompanySessionData companySessionData)
	{
		Company = companySessionData;
		NotifyChanged()
	}

	public void Clear ()
	{
		User = null;
		Company = null;
		NotifyChanged();
	}

	public event Action? OnChange;

	private void NotifyChanged () => OnChange?.Invoke();

}
