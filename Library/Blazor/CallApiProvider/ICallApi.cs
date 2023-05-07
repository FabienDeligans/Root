using Library.Models;

namespace Library.Blazor.CallApiProvider
{
    public interface ICallApi<T> where T : IEntity
    {
        string GetTypeName();
        Task DropCollectionAsync();
        Task<long> CountDataAsync();
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetOneFullAsync(string id);
        Task<T?> GetOneSimpleAsync(string id);
        Task DeleteOneAsync(string id);
        Task<T> UpdateAsync(T entityUpdate);
        Task<T> UpdatePropertyAsync(string id, Dictionary<string, string> propertyValueDictionary);
        Task<string> CatchError500(Exception e);
        Task<string> CatchError400(Exception e);
    }
}
