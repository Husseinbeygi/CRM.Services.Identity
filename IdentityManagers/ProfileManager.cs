using CRM.Services.Identity.Infrastructure.Middlewares;
using CRM.Services.Identity.Infrastructure.Settings;
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
		private readonly ApplicationSettings _applicationSettings;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ProfileManager(UserManager<User> userManager, ApplicationSettings applicationSettings, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_applicationSettings = applicationSettings;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var user = await _userManager.GetUserAsync(context.Subject);


			var supportedCultureNames =
				_applicationSettings.CultureSettings.SupportedCultureNames;

			var defaultCultureName =
					_applicationSettings.CultureSettings.DefaultCultureName;

			var currentCookieCulture = CultureCookieHandlerMiddleware
				.GetCultureNameByCookie(_httpContextAccessor.HttpContext, supportedCultureNames);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? "NoNameIdentifier"),
				new Claim(ClaimTypes.Actor, user?.UserName ?? "NoActor"),
				new Claim(ClaimTypes.GivenName, user?.FirstName ?? "NoGivenName"),
				new Claim(ClaimTypes.Surname, user?.LastName ?? "NoSurname"),
				new Claim(ClaimTypes.Name, string.Concat(user?.FirstName," ",user?.LastName)),
				new Claim("oid", user.OrganizationId.ToString() ?? default),
				new Claim("buid", user.BusinessUnitId.ToString() ?? default),
				new Claim(ClaimTypes.Locality,currentCookieCulture ?? defaultCultureName),
				new Claim("CurrentUICulture",currentCookieCulture ?? defaultCultureName)
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
