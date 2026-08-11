using System.Security.Claims;

namespace InnoClinic.Shared.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var value = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(value, out var userId))
                throw new UnauthorizedAccessException();

            return userId;
        }

        public static string GetUserRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new UnauthorizedAccessException();
        }
    }
}
