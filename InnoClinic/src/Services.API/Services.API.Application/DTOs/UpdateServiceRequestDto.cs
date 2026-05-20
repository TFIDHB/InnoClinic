using Domain.Enums;

namespace Application.DTOs
{
    public class UpdateServiceRequestDto
    {
        public string Name { get; set; }
        public double Price { get; set; }

        /// <example>a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1</example>
        public Guid ServiceCategoryId { get; set; }
        public ServiceStatus Status { get; set; }
    }
}
