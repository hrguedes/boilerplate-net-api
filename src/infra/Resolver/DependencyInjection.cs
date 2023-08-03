using Application.Autenticacao;
using Application.Autenticacao.Interfaces;
using Application.Company;
using Application.Company.Interfaces;
using Data.Repositories;
using Data.Repositories.Autenticacao;
using Entities.Autenticacao.Interfaces;
using Entities.Intefaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Resolver;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database

        // Services
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAutenticarUsuarioService, AutenticarUsuarioService>();


        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRegraRepository, RegraRepository>();
        services.AddScoped<ITelaRepository, TelaRepository>();
        services.AddScoped<ISessaoRepository, SessaoRepository>();
        
        // Integrations

        // massTransit
        
        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // return
        return services;
    }
}