using Application.Company;
using Application.Company.Interfaces;
using Data.Repositories;
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

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        // Integrations

        // massTransit

        // return
        return services;
    }
}