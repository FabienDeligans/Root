using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class OpeLogic : BaseApiLogic<Ope>
{
    public OpeLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}