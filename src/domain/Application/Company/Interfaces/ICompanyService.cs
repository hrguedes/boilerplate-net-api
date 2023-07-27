using Base;
using Dto.Company.Request;
using Dto.Company.Response;

namespace Application.Company.Interfaces;

public interface ICompanyService
{
    Task<ReturnOk<CompanyResponse>> CreateNewCompany(CreateNewCompanyRequest request);
    Task<ReturnOk<CompanyResponse>> EditCompany(EditCompanyRequest request);
    Task<ReturnOk<CompanyResponse>> RemoveCompany(Guid id);
    Task<ReturnOk<List<CompanyResponse>>> ListAllCompanies(ListCompaniesRequest request);
}