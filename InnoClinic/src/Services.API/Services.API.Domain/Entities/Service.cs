namespace Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public Guid? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
        public bool IsActive { get; set; }
    }
}
