using Domain.Enums;

namespace Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public ServiceStatus Status { get; set; }
    }
}
