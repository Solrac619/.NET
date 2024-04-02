using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    internal class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITenantInfo _currentTenant;

        public ApplicationDbInitializer(ApplicationDbContext dbContext, ITenantInfo currentTenant)
        {
            _dbContext = dbContext;
            _currentTenant = currentTenant;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }

                if (await _dbContext.Database.CanConnectAsync(cancellationToken))
                {

                }
            }
        }
    }
}
