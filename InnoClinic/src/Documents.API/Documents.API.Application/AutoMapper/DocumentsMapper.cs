using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
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
