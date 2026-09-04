using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, string role);

        string GenerateRefreshToken();
    }
}
