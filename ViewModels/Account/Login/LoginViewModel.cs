using System.ComponentModel.DataAnnotations;

namespace CRM.Services.Identity.ViewModels.Account.Login
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress]
		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.EmailAddress))]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.Password))]
		public string Password { get; set; }

		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.RememberMe))]
		public bool RememberMe { get; set; }

	}
}
