using System.ComponentModel.DataAnnotations;

namespace Identidade.ViewModels
{
    public class RefreshTokenViewModel
    {
        public RefreshTokenViewModel()
        {
            Id = Guid.NewGuid();
            Token = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Username { get; set; }
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
