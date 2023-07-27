using Base;
using Dto.Company.Request;
using Dto.Company.Response;

namespace Application.Company.Interfaces;

public interface ICompanyService
{
    Task<ReturnOk<CompanyResponse>> CreateNewCompany(CreateNewCompanyRequest request);
}