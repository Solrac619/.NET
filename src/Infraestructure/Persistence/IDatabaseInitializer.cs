namespace Infraestructure.Persistence
{
    internal interface IDatabaseInitializer
    {
        Task InitializeDatabasesAsync(CancellationToken cancellationToken);
        Task InitializeApplicationDbForTenantAsync(Tenant.TenantInfo tenant, CancellationToken cancellationToken);
    }
}
