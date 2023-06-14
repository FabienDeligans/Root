using System.Collections;
using System.Linq.Expressions;
using Back._Providers.DatabaseProvider;
using Common.Models;
using Common.Models.CustomAttribute;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _Providers.DatabaseProviders.MongoDb
{
    public class ServiceMongoDatabase : IApiServiceDatabase, IDisposable
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private string DatabaseName { get; set; }

        public ServiceMongoDatabase(IOptions<SettingsServiceMongoDb> settings)
        {
            Client = new MongoClient(settings.Value.ConnectionString);
            Database = Client.GetDatabase(settings.Value.DatabaseName);
            DatabaseName = settings.Value.DatabaseName;
        }

        private IMongoCollection<T> Collection<T>() where T : IEntity => Database.GetCollection<T>(typeof(T).Name);

        public async Task DropCollectionAsync<T>() where T : IEntity
        {
            await Database
                .DropCollectionAsync(typeof(T).Name)
                .ConfigureAwait(false);
        }

        public async Task<long> CountDataAsync<T>() where T : IEntity
        {
            return await Collection<T>()
                .CountDocumentsAsync(FilterDefinition<T>.Empty)
                .ConfigureAwait(false);
        }

        public async Task<T> CreateAsync<T>(T entity) where T : IEntity
        {
            entity.CreationDate = DateTime.Now.ToLocalTime();
            await Collection<T>()
                        .InsertOneAsync(entity)
                        .ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<T>> CreateManyAsync<T>(IEnumerable<T> entities) where T : IEntity
        {
            await Collection<T>()
                .InsertManyAsync(entities)
                .ConfigureAwait(false);

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : IEntity
        {
            var result = await Collection<T>()
                .Find(_ => true)
                .ToListAsync()
                .ConfigureAwait(false);
            return result.AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetAllFiltered<T>(Expression<Func<T, bool>> predicate) where T : IEntity
        {
            var filter = Builders<T>.Filter.Where(predicate);
            var result = await Collection<T>().FindAsync(filter);
            return result.ToEnumerable();
        }

        public async Task<T> GetOneAsync<T>(string id) where T : IEntity
        {
            return await Collection<T>()
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<T> UpdateAsync<T>(T entityUpdate) where T : IEntity
        {
            entityUpdate.UpdateDate = DateTime.Now.ToLocalTime();

            await Collection<T>()!
                .ReplaceOneAsync(x => x!.Id == entityUpdate!.Id, entityUpdate)
                .ConfigureAwait(false);
            return entityUpdate;
        }

        public async Task<T> UpdatePropertyAsync<T>(string id, Dictionary<string, object> propertyValueDictionary) where T : IEntity
        {
            propertyValueDictionary.Add(nameof(IEntity.UpdateDate), DateTime.Now.ToLocalTime());
            foreach (var (key, value) in propertyValueDictionary)
            {
                var filter = Builders<T>.Filter.Eq(v => v.Id, id);
                var update = Builders<T>.Update.Set(key, value);
                await Collection<T>().FindOneAndUpdateAsync(filter, update);
            }

            return await GetOneAsync<T>(id);
        }

        public async Task DeleteOneAsync<T>(string id) where T : IEntity
        {
            await Collection<T>()
                .DeleteOneAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// If entity contain ForeignKey
        /// Get the entity linked to the ForeignKey
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> GetEntityWithForeignKey<T>(T entity) where T : IEntity
        {
            try
            {
                var type = entity.GetType();
                var properties = type.GetProperties();
                foreach (var propertyInfo in properties)
                {
                    var attributes = propertyInfo.GetCustomAttributes(true);
                    foreach (var attribute in attributes.Where(v => v.GetType() == typeof(ForeignKeyAttribute)))
                    {
                        if (attribute is not ForeignKeyAttribute fkAttribute) continue;
                        var typeOfForeignKey = fkAttribute.TheType;
                        var valueOfForeignKey = propertyInfo.GetValue(entity);

                        var methodInfo = GetType().GetMethod(nameof(GetOneAsync));
                        var genericMethod = methodInfo?.MakeGenericMethod(typeOfForeignKey);
                        var task = (Task)genericMethod?.Invoke(this, new[] { valueOfForeignKey })!;
                        if (task == null) continue;

                        var propToUpdate = properties.FirstOrDefault(v => v.PropertyType == typeOfForeignKey);

                        await task.ConfigureAwait(false);
                        var result = task.GetType().GetProperty("Result");
                        var endResult = (IEntity)result!.GetValue(task)!;

                        propToUpdate?.SetValue(entity, endResult);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return entity;
        }

        /// <summary>
        /// Get a collection of linked IEntity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> GetCollectionEntity<T>(T entity) where T : IEntity
        {
            try
            {
                var type = entity.GetType();
                var properties = type.GetProperties();

                //Pour chaque propriété de Type GenericType de "entity"
                foreach (var propertyInfo in properties.Where(v => v.PropertyType.Namespace == "System.Collections.Generic"))
                {
                    if (propertyInfo.PropertyType.GenericTypeArguments[0].BaseType != typeof(Entity)) continue;
                    // définition du type de la propriété générique
                    var typeOfIEnumerable = propertyInfo.PropertyType.GenericTypeArguments;

                    // création de la méthode par réflexion
                    var methodInfo = GetType().GetMethod(nameof(this.GetAllAsync));
                    var genericMethod = methodInfo!.MakeGenericMethod(typeOfIEnumerable);

                    // exécution de la méthode
                    var task = (Task)genericMethod?.Invoke(this, Array.Empty<object>())!;
                    if (task == null) continue;

                    // retourne une liste de tâche casté en IEnumerable de IEntity
                    await task.ConfigureAwait(false);
                    var entities = task.GetType().GetProperty("Result");
                    var endResult = (IEnumerable<IEntity>)entities!.GetValue(task)!;

                    // création d'une liste générique dans laquel on ajoutera les tâches résolues (plus facile à manipuler)
                    var genericList = Activator.CreateInstance(typeof(List<>).MakeGenericType(typeOfIEnumerable));
                    genericList!.GetType().GetMethod("AddRange")?.Invoke(genericList, new[] { endResult });

                    // création d'une liste générique qui contiendra les résultats filtrés
                    var results = Activator.CreateInstance(typeof(List<>).MakeGenericType(typeOfIEnumerable));
                    foreach (var result in (IEnumerable)genericList)
                    {
                        if (result.GetType().GetProperty($"{type.Name}Id")!.GetValue(result)!.ToString() == entity.Id)
                        {
                            results!.GetType().GetMethod("Add")?.Invoke(results, new[] { result });
                        }
                    }

                    // Assignation de cette liste à la propriété concernée
                    propertyInfo.SetValue(entity, results);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return entity;
        }

        public void Dispose()
        {

        }
    }
}
