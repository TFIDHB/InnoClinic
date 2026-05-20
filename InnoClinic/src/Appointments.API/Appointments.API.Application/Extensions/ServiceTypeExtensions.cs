using Domain.Enums;

namespace Application.Extensions
{
    public static class ServiceTypeExtensions
    {
        public static int GetRequiredSlots(this ServiceType type) => type switch
        {
            ServiceType.Analyses => 1,
            ServiceType.Consultation => 2,
            ServiceType.Diagnostics => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}
