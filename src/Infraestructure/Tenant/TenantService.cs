using ApplicationCore.Interfaces;
using Finbuckle.MultiTenant;

namespace Infraestructure.Tenant
{
    public class TenantService : ITenantService
    {
        private readonly IMultiTenantStore<TenantInfo> _tenantStore;

        public TenantService(
            IMultiTenantStore<TenantInfo> tenantStore)
        {
            _tenantStore = tenantStore;
        }



        public async Task<bool> ExistsWithIdAsync(string id) =>
            await _tenantStore.TryGetAsync(id) is not null;

        public async Task<bool> ExistsWithNameAsync(string name) =>
            (await _tenantStore.GetAllAsync()).Any(t => t.Name == name);



    }
}
