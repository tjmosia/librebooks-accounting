using Librebooks.Models.Entity.IdentitySpace;
namespace Librebooks.Areas.Identity.Models.DataTransferObjects;

public readonly struct FindUserDTO (User user)
{
	public readonly string? Email = user.Email;
	public readonly string? FirstName = user.Name;
	public readonly string? LastName = user.Surname;
	public readonly string? Photo = user.Photo;
}
