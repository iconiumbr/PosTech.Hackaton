using Application.Gateways.DataAccess;
using Data.Interceptors;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();


            services.AddScoped<IRepositoryDoctor, RepositoryDoctor>();
            services.AddScoped<IRepositoryHoliday, RepositoryHoliday>();
            services.AddScoped<IRepositoryAppointment, RepositoryAppointment>();

            services.AddSingleton(TimeProvider.System);

            services.AddDbContext<HackatonDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
