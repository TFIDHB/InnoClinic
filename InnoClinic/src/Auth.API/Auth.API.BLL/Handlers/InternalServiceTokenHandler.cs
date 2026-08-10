using BLL.Interfaces;
using System.Net.Http.Headers;

namespace BLL.Handlers
{
    public class InternalServiceTokenHandler(ITokenService tokenService) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var token = tokenService.GenerateInternalServiceToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return base.SendAsync(request, ct);
        }
    }
}
