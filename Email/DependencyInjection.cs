using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LimpoExpress.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            return services;
        }
    }
}
