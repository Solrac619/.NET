namespace ApplicationCore.Interfaces
{
    public interface ITenantService
    {
        Task<bool> ExistsWithIdAsync(string id);
        Task<bool> ExistsWithNameAsync(string name);
    }
}
