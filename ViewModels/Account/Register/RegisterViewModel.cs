using System.ComponentModel.DataAnnotations;

namespace CRM.Services.Identity.ViewModels.Account.Register
{
    public class RegisterViewModel
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
