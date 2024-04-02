using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Settings
{
    public static class Startup
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)))
                    .Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            return services;
        }
    }
}
