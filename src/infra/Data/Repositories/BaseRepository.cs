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
        Collection = new MongoClient(LaunchSettings.MONGO_CONNECTION_STRING)
            .GetDatabase(LaunchSettings.NOME_BANCO_BASE)
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

    public T LoadRecordById(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        return Collection.Find(filter).FirstOrDefault();
    }

    public async Task<T> LoadRecordByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public void UpdateRecord(string id, T record)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        Collection.ReplaceOne(filter, record);
    }

    public async Task UpdateRecordAsync(string id, T record)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        await Collection.ReplaceOneAsync(filter, record);
    }

    public void DeleteRecord(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        Collection.DeleteOne(filter);
    }

    public async Task DeleteRecordAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        await Collection.DeleteOneAsync(filter);
    }
}