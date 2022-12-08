using System.ComponentModel.DataAnnotations;

namespace CRM.Services.Identity.ViewModels.Account.Register
{
    public class RegisterViewModels
    {

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.EmailAddress))]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Password))]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.ConfirmPassword))]

        [System.ComponentModel.DataAnnotations.Compare
        (otherProperty: nameof(Password),
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]
        public string ConfirmPassword { get; set; }
    }

}
