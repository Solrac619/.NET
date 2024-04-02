using ApplicationCore.Interfaces;
using Infraestructure.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Auth.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly TenantContext _tenantContext;
        public PermissionService(TenantContext tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public async Task<bool> HasPermission(string permission, int? userId)
        {
            bool hasPermission = false;
            Console.WriteLine(_tenantContext.Database.GetConnectionString());
            var user = await _tenantContext.User.FindAsync(userId);

            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
            {
                var userRoles = await _tenantContext.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

                foreach (var role in userRoles)
                {
                    if (await _tenantContext.RoleClaims.AnyAsync(x => x.ClaimType.ToLower() == "permission" && x.RoleId == role.RoleId && x.ClaimValue.ToLower() == permission.ToLower()))
                    {
                        hasPermission = true;
                        break;
                    }
                }
                if (!hasPermission)
                {
                    hasPermission = await _tenantContext.UserClaims.AnyAsync(x => x.ClaimType.ToLower() == "permission" && x.UserId == user.Id && x.ClaimValue.ToLower() == permission.ToLower());
                }
            }
            return hasPermission;
        }

        public async Task<List<string>> GetAllPermissions(string resource, int? userId)
        {
            var roles = await _tenantContext.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
            var permissions = await _tenantContext.RoleClaims.Where(x => x.ClaimValue.ToLower().StartsWith($"permission.{resource.ToLower()}.") && roles.Contains(x.RoleId)).AsNoTracking().ToListAsync();
            return permissions.Select(x => x.ClaimValue.Split($"Permission.{resource}.")[1]).ToList();

        }

    }
}
