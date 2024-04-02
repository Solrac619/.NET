using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Infraestructure.Auth.Permissions
{
    #region PermissionAttribute
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string resource, string action) =>
            Policy = $"Permission.{resource}.{action}";
    }

    #endregion

    #region PermissionPolicyProvider
    internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult<AuthorizationPolicy?>(policy.Build());
            }

            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);
    }

    #endregion

    #region PermissionRequirement
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
    #endregion

    #region PermissionAuthorizationHandler
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        public PermissionAuthorizationHandler(IPermissionService permissionService) => _permissionService = permissionService;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var usr = context.User?.GetUserId();
            if (!string.IsNullOrEmpty(usr))
            {
                if (await _permissionService.HasPermission(requirement.Permission, Convert.ToInt32(usr)))
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
    #endregion

}
