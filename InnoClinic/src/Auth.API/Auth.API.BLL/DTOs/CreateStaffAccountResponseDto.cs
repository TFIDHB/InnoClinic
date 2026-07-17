namespace BLL.DTOs
{
    public class CreateStaffAccountResponseDto
    {
        public required Guid AccountId { get; set; }
        public required string TemporaryFakePassword { get; set; }
    }
}
