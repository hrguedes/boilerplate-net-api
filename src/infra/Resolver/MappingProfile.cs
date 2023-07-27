using AutoMapper;
using Dto.Company.Request;
using Dto.Company.Response;
using Entities;

namespace Resolver;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyResponse>().ReverseMap();
        CreateMap<CreateNewCompanyRequest, CompanyResponse>().ReverseMap();
        CreateMap<EditCompanyRequest, Company>().ReverseMap();
        CreateMap<EditCompanyRequest, CompanyResponse>().ReverseMap();
        CreateMap<Company, CreateNewCompanyRequest>().ReverseMap();
    }
}