using AutoMapper;
using Documents.API.Application.DTOs;
using Documents.API.Domain.Entities;

namespace Documents.API.Application.AutoMapper
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
