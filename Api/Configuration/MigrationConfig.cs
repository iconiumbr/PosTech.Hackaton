using Data;
using Identidade;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration
{

    public static class MigrationConfig
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<HackatonDbContext>();
                db.Database.Migrate();
            }

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PostechIdentityDbContext>();
                db.Database.Migrate();
            }
        }
    }

}
