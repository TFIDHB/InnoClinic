namespace Domain.Entities
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Service> Services { get; set; } = [];
    }
}
