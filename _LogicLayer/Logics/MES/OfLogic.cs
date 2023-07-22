using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class OfLogic : BaseApiLogic<Of>
{
    public OfLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}