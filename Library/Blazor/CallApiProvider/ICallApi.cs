using Library.Models;

namespace Library.Blazor.CallApiProvider
{
    public interface ICallApi<T> where T : IEntity
    {
        string GetTypeName();
        Task<string> CatchExceptions(Exception exception);
        Task DropCollectionAsync();
        Task<long> CountDataAsync();
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<IEnumerable<T>?> GetAllFilteredByPropertyEqualAsync(string propertyName, string value);
        Task<T?> GetOneFullAsync(string id);
        Task<T?> GetOneSimpleAsync(string id);
        Task DeleteOneAsync(string id);
        Task<T> UpdateAsync(T entityUpdate);
        Task<T> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary);
        Task<string> CatchResponseNull(Exception e);
        Task<string> CatchBadRequest(Exception e);
    }
}
