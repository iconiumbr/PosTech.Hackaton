using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class ChangePasswordViewModel
    {

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }

}
