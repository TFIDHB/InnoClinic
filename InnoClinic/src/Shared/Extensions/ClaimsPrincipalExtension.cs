using System.Security.Claims;

namespace InnoClinic.Shared.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal) 
        {
            var value = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();
            return Guid.Parse(value);
        }

        public static string GetUserRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new UnauthorizedAccessException();
        }
    }
}
