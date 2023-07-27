using Dto.Base;

namespace Dto.Company.Request;

public class EditCompanyRequest : BaseDto
{
    public string FullName { get; set; }
    public string SocialName { get; set; }
    public string Document { get; set; }
}