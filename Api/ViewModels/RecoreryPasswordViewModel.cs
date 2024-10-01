using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class RecoreryPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
