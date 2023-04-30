using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Interfaces
{
    public interface IApiLogic<T>
    {
        Task DropCollectionAsync();
        Task<long> CountDataAsync();
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneFullAsync(string id);
        Task<T> GetOneSimpleAsync(string id);
        Task<T> UpdateAsync(T entityUpdate);
        Task<T> UpdatePropertyAsync(string id, Dictionary<string, string> propertyValueDictionary);
        Task DeleteOneAsync(string id);
    }
}
