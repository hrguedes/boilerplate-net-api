using Entities;
using Entities.Intefaces;
using MongoDB.Driver;

namespace Data.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public async Task<Company> SearchCompany(string document, string name, string fullname)
    {
        var filter = Builders<Company>
            .Filter.Where(x => x.Document == document 
                               || x.FullName == fullname 
                               || x.SocialName == name);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
}