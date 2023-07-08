using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.Business;

namespace _LogicLayer.Logics
{
    public class ParentLogic : BaseApiLogic<Parent>
    {
        public ParentLogic(
            IApiServiceDatabase serviceDatabase) 
            : base(serviceDatabase)
        {
        }
    }
}
