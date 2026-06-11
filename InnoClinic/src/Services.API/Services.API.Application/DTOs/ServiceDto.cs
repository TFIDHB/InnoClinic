namespace Application.DTOs
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
