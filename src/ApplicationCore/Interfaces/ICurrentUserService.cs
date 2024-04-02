using System.Security.Claims;

namespace ApplicationCore.Interfaces
{
    public interface ICurrentUserService
    {
        string? Name { get; }

        int? GetUserId();

        string? GetTenant();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim>? GetUserClaims();
    }
}
