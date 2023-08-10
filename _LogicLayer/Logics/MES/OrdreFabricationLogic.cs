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

    public override async Task<IEnumerable<OrdreFabrication>> GetAllAsync()
    {
        var ofs = await ServiceDatabase.GetAllAsync<OrdreFabrication>();

        var ofsToReturn = new List<OrdreFabrication>();
        foreach (var item in ofs)
        {
            ofsToReturn.Add(await GetOneFullAsync(item.Id)); 
        }

        return ofsToReturn;
    }

    public override async Task<OrdreFabrication> GetOneFullAsync(string id)
    {
        var of = await GetOneSimpleAsync(id);
        return of;
    }

    public override async Task<OrdreFabrication> UpdateAsync(OrdreFabrication entityUpdate)
    {
        foreach (var articlesConsomme in entityUpdate.EtapesExecuted.Last().ArticlesConsommes)
        {
            articlesConsomme.DateUsed = DateTime.Now;
        }

        if (entityUpdate.EtapesExecuted.Count() == entityUpdate.Gamme.Etapes.Count)
        {
            var end = false;
            foreach (var etapeExecuted in entityUpdate.EtapesExecuted)
            {
                if (etapeExecuted.End == default)
                {
                    end = false;
                }
                else
                {
                    end = true;
                }
            }

            if (end == true)
            {
                var articleToMake = await _articleLogic.GetOneSimpleAsync(entityUpdate.ArticleId);
                articleToMake.Stock += 1;

                await ServiceDatabase.UpdateAsync(articleToMake);
                entityUpdate.DateFin = DateTime.Now;
                entityUpdate.IsDisabled = true;
            }

        }

        await ServiceDatabase.UpdateAsync(entityUpdate);

        return entityUpdate;
    }

    public override async Task<OrdreFabrication> CreateAsync(OrdreFabrication entity)
    {
        // enregistrement de la gamme dans l'of
        var gamme = await _gammeLogic.GetOneSimpleAsync(entity.GammeId);
        gamme.Etapes = gamme.Etapes.OrderBy(v => v.Order).ToList();
        entity.Gamme = gamme;

        // enregistrement de l'article à fabriquer dans l'of
        entity.ArticleId = gamme.ArticleId;
        entity.ArticleFabrique = await _articleLogic.GetOneSimpleAsync(entity.ArticleId);

        // set un nouveau n° d'of
        entity.Number = await CountDataAsync() +1; 

        entity.DateDebut = DateTime.Now;
        entity.EtapesExecuted = new List<EtapeExecuted>();

        entity = await ServiceDatabase.CreateAsync(entity);

        return entity;
    }
}