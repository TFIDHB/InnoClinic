namespace InnoClinic.Shared.Interfaces
{
    public interface IBasicUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
