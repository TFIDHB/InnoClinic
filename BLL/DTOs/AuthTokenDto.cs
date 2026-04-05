namespace BLL.DTOs
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenId { get; set; }
    }
}
