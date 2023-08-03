namespace Entities.Intefaces;

public interface IBaseRepository<T>
{
    void InsertRecord(T record);
    Task InsertRecordAsync(T record);
    List<T> LoadRecords();
    Task<List<T>> LoadRecordsAsync();
    T LoadRecordById(string id);
    Task<T> LoadRecordByIdAsync(string id);
    void UpdateRecord(string id, T record);
    Task UpdateRecordAsync(string id, T record);
    void DeleteRecord(string id);
    Task DeleteRecordAsync(string id);
}