using ApplicationCore.Interfaces;
using Infraestructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Infraestructure.Tenant
{
    public static class Startup
    {
        public static IServiceCollection AddMultitenancy(this IServiceCollection services, IConfiguration config)
        {
            // TODO: We should probably add specific dbprovider/connectionstring setting for the tenantDb with a fallback to the main databasesettings
            var databaseSettings = config.GetSection(nameof(DataBaseSetting)).Get<DataBaseSetting>();
            string? rootConnectionString = databaseSettings.ConnectionString;
            if (string.IsNullOrEmpty(rootConnectionString)) throw new InvalidOperationException("DB ConnectionString is not configured.");

            return services
                .AddDbContext<TenantContext>(m => m.UseSqlServer(rootConnectionString))
                .AddMultiTenant<TenantInfo>()
                    .WithClaimStrategy("tenant")
                    .WithHeaderStrategy("tenant")
                    .WithQueryStringStrategy("tenant")
                    .WithEFCoreStore<TenantContext, TenantInfo>()
                    .Services
                .AddScoped<ITenantService, TenantService>();
        }

        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app) =>
            app.UseMultiTenant();

        private static FinbuckleMultiTenantBuilder<TenantInfo> WithQueryStringStrategy(this FinbuckleMultiTenantBuilder<TenantInfo> builder, string queryStringKey) =>
            builder.WithDelegateStrategy(context =>
            {
                if (context is not HttpContext httpContext)
                {
                    return Task.FromResult((string?)null);
                }

                httpContext.Request.Query.TryGetValue(queryStringKey, out StringValues tenantIdParam);

                return Task.FromResult((string?)tenantIdParam.ToString());
            });
    }
}
