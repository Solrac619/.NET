using ApplicationCore.Interfaces;
using Infraestructure.Services;
//using Infraestructure.Services;
using Infraestructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Persistence
{
    public static class Startup
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            // TODO: there must be a cleaner way to do IOptions validation...
            var databaseSettings = config.GetSection(nameof(DataBaseSetting)).Get<DataBaseSetting>();
            string? rootConnectionString = databaseSettings.ConnectionString;
            if (string.IsNullOrEmpty(rootConnectionString))
            {
                throw new InvalidOperationException("DB ConnectionString no esta configurado.");
            }

            services
                .Configure<DataBaseSetting>(config.GetSection(nameof(DataBaseSetting)))
                .AddDbContext<ApplicationDbContext>(m => m.UseSqlServer(rootConnectionString))
                .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
                .AddTransient<ApplicationDbInitializer>();

            //Add services
            services.AddTransient<IDashboardService, DashboardService>();

            //End services

            return services;

        }

    }
}
