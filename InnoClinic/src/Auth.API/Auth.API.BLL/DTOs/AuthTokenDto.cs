namespace BLL.DTOs
{
    public class AuthTokenDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
