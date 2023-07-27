namespace Entities.Intefaces;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<Company> SearchCompany(string document, string name, string fullname);
}