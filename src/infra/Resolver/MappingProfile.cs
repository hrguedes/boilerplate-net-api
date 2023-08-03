using AutoMapper;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Dto.Company.Request;
using Dto.Company.Response;
using Entities;
using Entities.Autenticacao;

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
        
        CreateMap<Regra, RegraResponse>().ReverseMap();
        CreateMap<Regra, CadastrarOuEditarRegraRequest>().ReverseMap();
        CreateMap<Tela, TelaResponse>().ReverseMap();
        CreateMap<Tela, CadastrarOuEditarTelaRequest>().ReverseMap();
        CreateMap<Usuario, CadastrarUsuarioRequest>().ReverseMap();
        CreateMap<Usuario, UsuarioResponse>().ReverseMap();
    }
}