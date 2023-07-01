using System.Linq.Expressions;

namespace _LogicLayer.Logics
{
    public interface ILogic<T>
    {
        Task DropCollectionAsync();
        Task<long> CountDataAsync();
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllFilteredByPropertyEqualAsync(string propertyName, string value);
        Task<IEnumerable<T>> GetAllFilteredByPropertyEqualAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetOneFullAsync(string id);
        Task<T> GetOneSimpleAsync(string id);
        Task<T> UpdateAsync(T entityUpdate);
        Task<T> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary);
        Task DeleteOneAsync(string id);
    }
}
