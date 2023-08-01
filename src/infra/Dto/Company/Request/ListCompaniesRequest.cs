using Base;

namespace Dto.Company.Request;

public class ListCompaniesRequest : Pagination
{
    public string id { get; set; }
    public string name { get; set; }
}