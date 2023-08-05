using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class OrdreFabricationLogic : BaseApiLogic<OrdreFabrication>
{
    private readonly ILogic<Article> _articleLogic;
    private readonly ILogic<Gamme> _gammeLogic;
    private readonly ILogic<Etape> _etapeLogic;
    private readonly ILogic<GammeEtape> _gammeEtapeLogic;


    public OrdreFabricationLogic(IApiServiceDatabase serviceDatabase,
        ILogic<Article> articleLogic,
        ILogic<Gamme> gammeLogic,
        ILogic<Etape> etapeLogic,
        ILogic<GammeEtape> gammeEtapeLogic)
        : base(serviceDatabase)
    {
        _articleLogic = articleLogic;
        _gammeLogic = gammeLogic;
        _etapeLogic = etapeLogic;
        _gammeEtapeLogic = gammeEtapeLogic;
    }

    public override async Task<OrdreFabrication> GetOneFullAsync(string id)
    {
        var of = await GetOneSimpleAsync(id);
        of.ArticleFabrique = await _articleLogic.GetOneSimpleAsync(of.ArticleId);
        of.Gamme = await _gammeLogic.GetOneSimpleAsync(of.GammeId);

        of.Gamme.Etapes = new List<Etape>();
        var gammeEtapes = await _gammeEtapeLogic.GetAllFilteredByPropertyEqualAsync(nameof(GammeEtape.GammeId), of.GammeId);

        foreach (var gammeEtape in gammeEtapes)
        {
            var etape = await _etapeLogic.GetOneSimpleAsync(gammeEtape.EtapeId); 
            of.Gamme.Etapes.Add(etape);
        }

        return of; 
    }

    public override async Task<OrdreFabrication> UpdateAsync(OrdreFabrication entityUpdate)
    {
        foreach (var articlesConsomme in entityUpdate.EtapesExecuted.Last().ArticlesConsommes)
        {
            articlesConsomme.DateUsed = DateTime.Now;

            var article = await _articleLogic.GetOneSimpleAsync(articlesConsomme.ArticleId);
            article.StockReserved -= articlesConsomme.QuantityToUse; 
            article.Stock -= articlesConsomme.QuantityUsed;
            await _articleLogic.UpdateAsync(article); 
        }

        if (entityUpdate.EtapesExecuted.Count() == entityUpdate.Gamme.Etapes.Count)
        {
            var articleToMake = await _articleLogic.GetOneSimpleAsync(entityUpdate.ArticleId);
            articleToMake.Stock += 1;

            await ServiceDatabase.UpdateAsync(articleToMake); 
            entityUpdate.DateFin = DateTime.Now;
        }

        await ServiceDatabase.UpdateAsync(entityUpdate);

        return entityUpdate; 
    }

    public override async Task<OrdreFabrication> CreateAsync(OrdreFabrication entity)
    {
        var gamme = await _gammeLogic.GetOneSimpleAsync(entity.GammeId);
        gamme.Etapes = new List<Etape>();

        var gammeEtapes = await _gammeEtapeLogic.GetAllFilteredByPropertyEqualAsync(nameof(GammeEtape.GammeId), entity.GammeId);
        foreach (var gammeEtape in gammeEtapes)
        {
            var etape = await _etapeLogic.GetOneSimpleAsync(gammeEtape.EtapeId);
            gamme.Etapes.Add(etape);

            foreach (var etapeArticlesConsomme in etape.ArticlesConsommes)
            {
                var article = await _articleLogic.GetOneSimpleAsync(etapeArticlesConsomme.ArticleId);
                article.StockReserved += etapeArticlesConsomme.QuantityToUse;
                
                await _articleLogic.UpdateAsync(article); 
            }

        }
        entity.Gamme = gamme;

        entity.ArticleId = gamme.ArticleId;
        entity.ArticleFabrique = await _articleLogic.GetOneSimpleAsync(entity.ArticleId);

        entity.DateDebut = DateTime.Now;
        entity.EtapesExecuted = new List<Etape>();

        entity = await ServiceDatabase.CreateAsync(entity); 

        return entity;
    }
}