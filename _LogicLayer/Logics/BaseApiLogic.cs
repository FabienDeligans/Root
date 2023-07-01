using System.Linq.Expressions;
using System.Web;
using _Providers.DatabaseProviders;
using Common.Models;

namespace _LogicLayer.Logics
{
    public abstract class BaseApiLogic<T> : ILogic<T> where T : IEntity
    {
        protected readonly IApiServiceDatabase ServiceDatabase;

        protected BaseApiLogic(IApiServiceDatabase serviceDatabase)
        {
            ServiceDatabase = serviceDatabase;
        }

        public virtual async Task DropCollectionAsync()
        {
            await ServiceDatabase.DropCollectionAsync<T>();
        }

        public virtual async Task<long> CountDataAsync()
        {
            return await ServiceDatabase.CountDataAsync<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            return await ServiceDatabase.CreateAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = DateTime.Now.ToLocalTime();
            }
            return await ServiceDatabase.CreateManyAsync(entities);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await ServiceDatabase.GetAllAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAllFilteredByPropertyEqualAsync(string propertyName, string value)
        {
            value = HttpUtility.UrlDecode(value.ToString())!;
            var parameter = Expression.Parameter(typeof(T));

            var type = typeof(T).GetProperty(propertyName)!.PropertyType;
            var left = Expression.Property(parameter, propertyName);

            var val = type.Name == "DateTime" ? Convert.ToDateTime(value) : Convert.ChangeType(value, type);
            var right = Expression.Constant(val);

            BinaryExpression? body = Expression.Equal(left, right);
            var predicate = Expression.Lambda<Func<T, bool>>(body!, parameter);

            return await ServiceDatabase.GetAllFiltered(predicate);
        }

        public async Task<IEnumerable<T>> GetAllFilteredByPropertyEqualAsync(Expression<Func<T, bool>> predicate)
        {
            return await ServiceDatabase.GetAllFiltered(predicate);
        }

        public virtual async Task<T> GetOneFullAsync(string id)
        {
            var entity = await ServiceDatabase.GetOneAsync<T>(id);

            entity = await ServiceDatabase.GetEntityWithForeignKey(entity);
            entity = await ServiceDatabase.GetCollectionEntity(entity);

            return entity;
        }

        public async Task<T> GetOneSimpleAsync(string id)
        {
            return await ServiceDatabase.GetOneAsync<T>(id);
        }

        public virtual async Task<T> UpdateAsync(T entityUpdate)
        {
            return await ServiceDatabase.UpdateAsync(entityUpdate);
        }

        public async Task<T> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary)
        {
            return await ServiceDatabase.UpdatePropertyAsync<T>(id, propertyValueDictionary);
        }

        public virtual async Task DeleteOneAsync(string id)
        {
            await ServiceDatabase.DeleteOneAsync<T>(id);
        }
    }
}
