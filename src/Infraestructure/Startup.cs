using Infraestructure.Auth;
using Infraestructure.Cors;
using Infraestructure.EventHandlers;
using Infraestructure.Persistence;
using Infraestructure.Settings;
using Infraestructure.Tenant;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger(configuration)
                    .AddSettings(configuration)
                    .AddHttpContextAccessor()
                    .AddPermissions()
                    .AddCurrentUser()
                    .AddExceptionMiddleware()
                    .AddMultitenancy(configuration)
                    .AddPersistence(configuration)
                    .AddJwt(configuration)
                    .AddEventHandlers()
                    .AddCorsPolicy();
            return services;
        }

        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
                .InitializeDatabasesAsync(cancellationToken);
        }

        public static IApplicationBuilder UseInfraestructure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackOffice api");
            });
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCorsPolicy();
            app.UseAuthentication();
            app.UseCurrentUser();
            app.UseMultiTenancy();
            app.UseAuthorization();

            app.UseExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
