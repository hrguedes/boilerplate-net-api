using Consts;
using Entities.Intefaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T>
{
    protected readonly IMongoCollection<T> Collection;

    protected BaseRepository()
    {
        Collection = new MongoClient(LaunchSettings.ConnectionString)
            .GetDatabase(LaunchSettings.Database)
            .GetCollection<T>(typeof(T).Name);
    }

    public void InsertRecord(T record)
    {
        Collection.InsertOne(record);
    }

    public async Task InsertRecordAsync(T record)
    {
        await Collection.InsertOneAsync(record);
    }

    public List<T> LoadRecords()
    {
        return Collection.Find(new BsonDocument()).ToList();
    }

    public async Task<List<T>> LoadRecordsAsync()
    {
        return await Collection.Find(new BsonDocument()).ToListAsync();
    }

    public T LoadRecordById(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return Collection.Find(filter).FirstOrDefault();
    }

    public async Task<T> LoadRecordByIdAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public void UpdateRecord(Guid id, T record)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        Collection.ReplaceOne(filter, record);
    }

    public async Task UpdateRecordAsync(Guid id, T record)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.ReplaceOneAsync(filter, record);
    }

    public void DeleteRecord(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        Collection.DeleteOne(filter);
    }

    public async Task DeleteRecordAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
}