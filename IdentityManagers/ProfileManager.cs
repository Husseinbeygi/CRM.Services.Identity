using CRM.Services.Identity.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CRM.Services.Identity.IdentityManagers
{
	public class ProfileManager : IProfileService
	{
		private readonly UserManager<User> _userManager;

		public ProfileManager(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var user = await _userManager.GetUserAsync(context.Subject);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user?.UserName ?? "NoNameIdentifier"),
				new Claim(ClaimTypes.GivenName, user?.FirstName ?? "NoGivenName"),
				new Claim(ClaimTypes.Surname, user?.LastName ?? "NoSurname"),
				new Claim(ClaimTypes.Name, string.Concat(user?.FirstName," ",user?.LastName)),
				new Claim("OrganizationId", user.OrganizationId.ToString() ?? default),
				new Claim("BusinessUnitId", user.BusinessUnitId.ToString() ?? default),
				new Claim(ClaimTypes.Locality, Thread.CurrentThread.CurrentCulture.Name),
				new Claim("CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name),

			};

			context.IssuedClaims.AddRange(claims);
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var user = await _userManager.GetUserAsync(context.Subject);

			context.IsActive = (user != null) && user.IsActive;
		}
	}
}
