namespace Application.Interfaces
{
    public interface IProfilesUnitOfWork
    {
        IProfilesRepository ProfilesRepository { get; }
    }
}
