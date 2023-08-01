using System.Text.RegularExpressions;
using Entities;
using Entities.Intefaces;
using MongoDB.Bson;
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

    public async Task<List<Company>> ListCompaniesByFilter(string nameOrFullName, string id)
    {
        var filter = Builders<Company>.Filter
            .Where(x => 
                x.FullName.Contains(nameOrFullName) || 
                x.SocialName.Contains(nameOrFullName) ||
                x.Id.Contains(id));
        return await Collection.Find(filter).ToListAsync();
    }
}