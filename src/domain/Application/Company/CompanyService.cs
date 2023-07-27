using Application.Company.Interfaces;
using Base;
using Dto.Company.Request;
using Dto.Company.Response;
using Entities.Intefaces;

namespace Application.Company;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public Task<ReturnOk<CompanyResponse>> CreateNewCompany(CreateNewCompanyRequest request)
    {
        throw new NotImplementedException();
    }
}