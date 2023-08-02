using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class EtapeLogic : BaseApiLogic<Etape>
{
    public EtapeLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}