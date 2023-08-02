using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class GammeEtapeLogic : BaseApiLogic<GammeEtape>
{
    public GammeEtapeLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}