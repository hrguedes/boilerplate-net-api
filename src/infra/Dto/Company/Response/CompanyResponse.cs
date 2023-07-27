using Dto.Base;

namespace Dto.Company.Response;

public class CompanyResponse : BaseDto
{
    public string FullName { get; set; }
    public string SocialName { get; set; }
    public string Document { get; set; }
}