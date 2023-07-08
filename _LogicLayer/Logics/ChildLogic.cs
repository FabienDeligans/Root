using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.Business;

namespace _LogicLayer.Logics
{
    public class ChildLogic : BaseApiLogic<Child>
    {
        public ChildLogic(IApiServiceDatabase serviceDatabase) 
            : base(serviceDatabase)
        {
        }
    }
}
