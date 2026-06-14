namespace LibrebooksBlazor.Client.Models
{
	public readonly struct IdentitySessionData (UserSessionData? user, CompanySessionData? company)
	{
		public readonly UserSessionData? user = user;
		public readonly CompanySessionData? company = company;
	}
}
