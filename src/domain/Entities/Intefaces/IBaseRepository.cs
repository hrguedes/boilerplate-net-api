namespace Entities.Intefaces;

public interface IBaseRepository<T>
{
    void InsertRecord(T record);
    Task InsertRecordAsync(T record);
    List<T> LoadRecords();
    Task<List<T>> LoadRecordsAsync();
    T LoadRecordById(Guid id);
    Task<T> LoadRecordByIdAsync(Guid id);
    void UpdateRecord(Guid id, T record);
    Task UpdateRecordAsync(Guid id, T record);
    void DeleteRecord(Guid id);
    Task DeleteRecordAsync(Guid id);
}