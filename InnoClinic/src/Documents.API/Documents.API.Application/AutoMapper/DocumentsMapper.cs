using AutoMapper;
using InnoClinic.Documents.API.Domain.Entities;
using InnoClinic.Documents.API.Application.DTOs;

namespace InnoClinic.Documents.API.Application.AutoMapper
{
    public class DocumentsMapper : Profile
    {
        public DocumentsMapper()
        {
            CreateMap<Photo, PhotoDto>();
            CreateMap<Document, DocumentDto>();
        }
    }
}
