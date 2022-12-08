using Microsoft.AspNetCore.Identity;

namespace CRM.Services.Identity.Models;

public class User : IdentityUser<Guid>
{
    public Guid OrganizationId { get; set; }
	public Guid BusinessUnitId { get; set; }
	public bool IsActive { get; set; }
    public string DefaultCulture { get; set; }
	public string? FirstName { get; internal set; }
	public string? LastName { get; internal set; }
}
