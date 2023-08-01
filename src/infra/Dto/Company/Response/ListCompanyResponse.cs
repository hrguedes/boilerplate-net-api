using Base;

namespace Dto.Company.Response;

public class ListCompanyResponse : Pagination
{
    public List<CompanyResponse> Data { get; set; }
}