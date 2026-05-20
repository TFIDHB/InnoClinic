namespace Application.DTOs
{
    public class CreateServiceRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ServiceCategory { get; set; }
    }
}
