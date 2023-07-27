using Entities.Base;

namespace Entities;

public class Company : BaseAudityEntity
{
    public string FullName { get; set; }
    public string SocialName { get; set; }
    public string Document { get; set; }
    public string InternalReferenceCode { get; set; }
}