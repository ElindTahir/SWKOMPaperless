using AutoMapper;
using NPaperless.Services.DTOs;
using Correspondent = NPaperless.DataAccess.Entities.Correspondent;
using Document = NPaperless.DataAccess.Entities.Document;
using DocumentType = NPaperless.DataAccess.Entities.DocumentType;
using Tag = NPaperless.DataAccess.Entities.Tag;
using UserInfo = NPaperless.DataAccess.Entities.UserInfo;

namespace NPaperless.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NewCorrespondentDTO, Correspondent>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.Ignore());
        CreateMap<Correspondent, CorrespondentDTO>().ReverseMap();
        CreateMap<Document, DocumentDTO>().ReverseMap();
        CreateMap<DocumentType, DocumentTypeDTO>().ReverseMap();
        CreateMap<Tag, TagDTO>().ReverseMap();
        CreateMap<UserInfo, UserInfoDTO>().ReverseMap();
        
        //TODO: Mapping for NewDTOs still missing
    }
    
}