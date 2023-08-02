using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class GammeLogic : BaseApiLogic<Gamme>
{
    public GammeLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}