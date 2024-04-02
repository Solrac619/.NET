using ApplicationCore.Interfaces;
using System.Security.Claims;

namespace Infraestructure.Auth.User
{
    public class CurrentUser : ICurrentUserService, ICurrentUserInitializerService
    {
        private ClaimsPrincipal? _user;

        public string? Name => _user?.Identity?.Name;

        private int? _userId = null;

        public int? GetUserId() =>
           IsAuthenticated()
               ? Convert.ToInt32(_user?.GetUserId())
               : _userId;

        public bool IsAuthenticated() =>
            _user?.Identity?.IsAuthenticated is true;

        public bool IsInRole(string role) =>
            _user?.IsInRole(role) is true;

        public IEnumerable<Claim>? GetUserClaims() =>
            _user?.Claims;

        public string? GetTenant() =>
            IsAuthenticated() ? _user?.GetTenant() : string.Empty;

        public void SetCurrentUser(ClaimsPrincipal user)
        {
            if (_user != null)
            {
                throw new Exception("Method reserved for in-scope initialization");
            }

            _user = user;
        }

        public void SetCurrentUserId(string userId)
        {
            if (_userId != null)
            {
                throw new Exception("Method reserved for in-scope initialization");
            }

            if (!string.IsNullOrEmpty(userId))
            {
                _userId = Convert.ToInt32(userId);
            }
        }
    }
}