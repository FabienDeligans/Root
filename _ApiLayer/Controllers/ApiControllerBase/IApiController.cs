using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.ApiControllerBase
{
    public interface IApiController<T> where T : IEntity
    {
        /// <summary>
        /// Efface une collection
        /// </summary>
        /// <returns></returns>
        Task<ActionResult> DropCollectionAsync();

        /// <summary>
        /// Compte le nombre d'enregistrement dans la collection
        /// </summary>
        /// <returns></returns>
        Task<ActionResult<long>> CountDataAsync();

        /// <summary>
        /// Cré un enregistrement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ActionResult<T>> CreateAsync(T entity);

        /// <summary>
        /// Cré plusieurs enregistrements
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<ActionResult<IEnumerable<T>>> CreateManyAsync(IEnumerable<T> entities);

        /// <summary>
        /// Retourne tous les enregistrements
        /// </summary>
        /// <returns></returns>
        Task<ActionResult<IEnumerable<T>>> GetAllAsync();

        /// <summary>
        /// Retourne tous les enregistrements dont la propriété est égale à la valeur
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<ActionResult<IEnumerable<T>?>> GetAllFilteredByPropertyEqualAsync(string propertyName, string value);

        /// <summary>
        /// Retour un object complet avec ses Foreignkey et ses listes dépendants d'autres objects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActionResult<T>> GetOneFullAsync(string id);

        /// <summary>
        /// Retourne un object simple
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActionResult<T>> GetOneSimpleAsync(string id);

        /// <summary>
        /// Met à jour un object
        /// </summary>
        /// <param name="entityUpdate"></param>
        /// <returns></returns>
        Task<ActionResult<T>> UpdateAsync(T entityUpdate);

        /// <summary>
        /// Met à jour la propriété d'un object
        /// </summary>
        /// <param name="entityUpdate"></param>
        /// <returns></returns>
        Task<ActionResult<T>> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary);

        /// <summary>
        /// Efface un object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActionResult> DeleteOneAsync(string id);

    }
}
