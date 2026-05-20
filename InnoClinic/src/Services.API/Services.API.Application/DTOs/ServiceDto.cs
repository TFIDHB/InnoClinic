namespace Application.DTOs
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ServiceCategory { get; set; }
        public string Status { get; set; }
    }
}
