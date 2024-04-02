namespace ApplicationCore.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> HasPermission(string permission, int? id);
        Task<List<string>> GetAllPermissions(string resource, int? userId);
    }
}
