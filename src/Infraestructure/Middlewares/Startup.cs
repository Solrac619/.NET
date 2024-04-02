using Infraestructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Middleware
{
    public static class Startup
    {
        public static IServiceCollection AddExceptionMiddleware(this IServiceCollection services) =>
            services.AddScoped<ExceptionMiddleware>();

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ExceptionMiddleware>();


    }
}