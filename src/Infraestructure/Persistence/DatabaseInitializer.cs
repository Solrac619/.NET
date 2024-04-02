using Finbuckle.MultiTenant;
using Infraestructure.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Persistence
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly TenantContext _tenantDbContext;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(TenantContext tenantDbContext, IServiceProvider serviceProvider)
        {
            _tenantDbContext = tenantDbContext;
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
        {
            await InitializeTenantDbAsync(cancellationToken);

            //foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
            //{
            //    await InitializeApplicationDbForTenantAsync(tenant, cancellationToken);
            //}

        }

        public async Task InitializeApplicationDbForTenantAsync(Tenant.TenantInfo tenant, CancellationToken cancellationToken)
        {
            // First create a new scope
            using var scope = _serviceProvider.CreateScope();

            // Then set current tenant so the right connectionstring is used
            _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>()
                .MultiTenantContext = new MultiTenantContext<Tenant.TenantInfo>()
                {
                    TenantInfo = tenant
                };

            // Then run the initialization in the new scope
            await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
                .InitializeAsync(cancellationToken);
        }

        private async Task InitializeTenantDbAsync(CancellationToken cancellationToken)
        {
            if (_tenantDbContext.Database.GetPendingMigrations().Any())
            {
                await _tenantDbContext.Database.MigrateAsync(cancellationToken);
            }

        }

    }
}
