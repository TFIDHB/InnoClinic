using InnoClinic.Shared.Interfaces;

namespace Documents.API.Application.Interfaces
{
    public interface IDocumentsUnitOfWork : IBasicUnitOfWork
    {
        IPhotosRepository PhotosRepository { get; }
        IDocumentsRepository DocumentsRepository { get; }
    }
}
