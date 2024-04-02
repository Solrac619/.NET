using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Cors
{
    public static class Startup
    {
        private static readonly string _namePolicy = "CorsPolicy";
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(p =>
               p.AddPolicy(_namePolicy, p =>
               {
                   p.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed(x => true)
                   .AllowCredentials();
               })
               );
            return services;
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(_namePolicy);
            return app;
        }
    }
}
