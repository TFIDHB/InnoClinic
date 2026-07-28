using Microsoft.AspNetCore.Http;

namespace Infrastructure.Clients
{
    public class AuthHeaderDelegationClient(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct = default)
        {
            var incomingAuthHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(incomingAuthHeader))
            {
                request.Headers.TryAddWithoutValidation("Authorization", incomingAuthHeader);
            }

            return base.SendAsync(request, ct);
        }
    }
}
