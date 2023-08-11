using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES
{
    public class ArticleLogic : BaseApiLogic<Article>
    {
        private readonly ILogic<Gamme> _gammeLogic;
        public ArticleLogic(IApiServiceDatabase serviceDatabase, ILogic<Gamme> gammeLogic)
            : base(serviceDatabase)
        {
            _gammeLogic = gammeLogic;
        }

        public override async Task<Article> GetOneFullAsync(string id)
        {
            var articleToReturn = await ServiceDatabase.GetOneAsync<Article>(id);

            if (articleToReturn.EstFabrique)
            {
                var gammesOfArticle = await _gammeLogic.GetAllFilteredByPropertyEqualAsync(nameof(Gamme.ArticleId), id);

                articleToReturn.GammesFabrication = new List<Gamme>();
                foreach (var gamme in gammesOfArticle)
                {
                    articleToReturn.GammesFabrication.Add(await _gammeLogic.GetOneFullAsync(gamme.Id));
                }
            }

            return articleToReturn;
        }
    }
}
