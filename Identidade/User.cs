using Microsoft.AspNetCore.Identity;

namespace Identidade
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
    }
}
