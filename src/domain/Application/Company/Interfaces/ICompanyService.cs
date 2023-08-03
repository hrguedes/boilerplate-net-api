using Base;
using Dto.Company.Request;
using Dto.Company.Response;

namespace Application.Company.Interfaces;

public interface ICompanyService
{
    Task<ReturnOk<CompanyResponse>> CreateNewCompany(CreateNewCompanyRequest request);
    Task<ReturnOk<CompanyResponse>> EditCompany(EditCompanyRequest request);
    Task<ReturnOk<CompanyResponse>> RemoveCompany(string id);
    Task<ReturnOk<ListCompanyResponse>> ListAllCompanies(ListCompaniesRequest request);
}