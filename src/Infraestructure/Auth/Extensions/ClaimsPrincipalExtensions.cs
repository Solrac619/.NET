

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {

        public static string? GetTenant(this ClaimsPrincipal principal)
            => principal.FindValue("tenant");

        public static string? GetFirstName(this ClaimsPrincipal principal)
            => principal?.FindFirst(ClaimTypes.Name)?.Value;

        public static string? GetSurname(this ClaimsPrincipal principal)
            => principal?.FindFirst(ClaimTypes.Surname)?.Value;

        public static string? GetUserId(this ClaimsPrincipal principal)
           => principal.FindValue("uid");

        private static string? FindValue(this ClaimsPrincipal principal, string claimType) =>
            principal is null
                ? throw new ArgumentNullException(nameof(principal))
                : principal.FindFirst(claimType)?.Value;
    }
}