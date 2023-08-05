using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class GammeLogic : BaseApiLogic<Gamme>
{

    private readonly ILogic<Etape> _etapeLogic;
    private readonly ILogic<GammeEtape> _gammeEtapeLogic;

    public GammeLogic(
        IApiServiceDatabase serviceDatabase,
        ILogic<Etape> etapeLogic,
        ILogic<GammeEtape> gammeEtapeLogic)
        : base(serviceDatabase)
    {
        _etapeLogic = etapeLogic;
        _gammeEtapeLogic = gammeEtapeLogic;
    }

    public override async Task<Gamme> GetOneFullAsync(string id)
    {
        var gamme = await GetOneSimpleAsync(id);
        gamme.Etapes = new List<Etape>();

        var gammeEtapes = await _gammeEtapeLogic.GetAllFilteredByPropertyEqualAsync(nameof(GammeEtape.GammeId), id);
        foreach (var gammeEtape in gammeEtapes)
        {
            var etape = await _etapeLogic.GetOneSimpleAsync(gammeEtape.EtapeId); 
            gamme.Etapes.Add(etape);
        }

        return gamme; 
    }
}