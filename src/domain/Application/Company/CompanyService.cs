using Application.Company.Interfaces;
using AutoMapper;
using Base;
using Dto.Company.Request;
using Dto.Company.Response;
using Entities.Intefaces;

namespace Application.Company;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<ReturnOk<CompanyResponse>> CreateNewCompany(CreateNewCompanyRequest request)
    {
        if (string.IsNullOrEmpty(request.FullName) 
            || string.IsNullOrEmpty(request.SocialName) 
            || string.IsNullOrEmpty(request.Document))
            return new ReturnOk<CompanyResponse>(null, new[]
            {
                "Please enter the required fields"
            }, false, 400);
        var existCompany = await _companyRepository.SearchCompany(request.Document, request.SocialName, request.FullName);
        if (existCompany != null)
        {
            return new ReturnOk<CompanyResponse>(null, new[]
            {
                "There is already a company with the same data"
            }, false, 400);
        }
        await _companyRepository.InsertRecordAsync(_mapper.Map<Entities.Company>(request));
        return new ReturnOk<CompanyResponse>(_mapper.Map<CompanyResponse>(request), new[]
        {
            "New Company created"
        });
    }

    public async Task<ReturnOk<CompanyResponse>> EditCompany(EditCompanyRequest request)
    {
        var company = await _companyRepository.LoadRecordByIdAsync(request.Id);
        if (company == null)
            return new ReturnOk<CompanyResponse>(_mapper.Map<CompanyResponse>(request), new[] { "Not found Company" }, false, 400);
        await _companyRepository.UpdateRecordAsync(request.Id, _mapper.Map<Entities.Company>(request));
        return new ReturnOk<CompanyResponse>(_mapper.Map<CompanyResponse>(request), new[] { "Company updated" });
    }

    public async Task<ReturnOk<CompanyResponse>> RemoveCompany(Guid id)
    {
        var company = await _companyRepository.LoadRecordByIdAsync(id);
        if (company == null)
            return new ReturnOk<CompanyResponse>(null, new[] { "Not found Company" }, false, 400);
        await _companyRepository.DeleteRecordAsync(id);
        return new ReturnOk<CompanyResponse>(_mapper.Map<CompanyResponse>(company), new[] { "Company removed" });
    }

    public Task<ReturnOk<List<CompanyResponse>>> ListAllCompanies(ListCompaniesRequest request)
    {
        throw new NotImplementedException();
    }
}