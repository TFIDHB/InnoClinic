namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
