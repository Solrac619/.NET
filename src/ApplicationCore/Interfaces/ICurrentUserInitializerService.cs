using System.Security.Claims;

namespace ApplicationCore.Interfaces
{
    public interface ICurrentUserInitializerService
    {
        void SetCurrentUser(ClaimsPrincipal user);

        void SetCurrentUserId(string userId);
    }
}