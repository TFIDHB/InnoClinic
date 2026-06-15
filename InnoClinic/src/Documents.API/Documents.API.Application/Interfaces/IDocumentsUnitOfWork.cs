using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IDocumentsUnitOfWork : IBasicUnitOfWork
    {
        IPhotosRepository PhotosRepository { get; }
        IDocumentsRepository DocumentsRepository { get; }
    }
}
