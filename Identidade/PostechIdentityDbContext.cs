using Identidade.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identidade
{
    public class PostechIdentityDbContext : IdentityDbContext<User>
    {
        public PostechIdentityDbContext(DbContextOptions<PostechIdentityDbContext> options) : base(options) { }

        public DbSet<RefreshTokenViewModel> RefreshTokens { get; set; }
    }
}
