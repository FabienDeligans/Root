using Library.Api.ApiDatabaseProvider;
using Library.Models;

namespace Library.Api.ApiLogicProvider
{
    public abstract class BaseApiLogic<T> : IApiLogic<T> where T : IEntity
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
            entity.CreationDate = DateTime.Now.ToLocalTime();
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

        public virtual async Task<T> GetOneFullAsync(string? id)
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
            entityUpdate.UpdateDate = DateTime.Now.ToLocalTime();
            return await ServiceDatabase.UpdateAsync(entityUpdate);
        }

        public async Task<T> UpdatePropertyAsync(string id, Dictionary<string, string> propertyValueDictionary)
        {
            propertyValueDictionary.Add(nameof(IEntity.UpdateDate), DateTime.Now.ToLocalTime().ToString());
            return await ServiceDatabase.UpdatePropertyAsync<T>(id, propertyValueDictionary);
        }

        public virtual async Task DeleteOneAsync(string id)
        {
            await ServiceDatabase.DeleteOneAsync<T>(id);
        }
    }
}
