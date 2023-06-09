using _Providers.DatabaseProviders.MongoDb;
using Library._LogicLayer.Logic;
using Library._Providers.DatabaseProvider;
using Library._Providers.Models.Business;

namespace _LogicLayer.Logics
{
    public class ParentLogic : BaseApiLogic<Parent>
    {
        public ParentLogic(
            ServiceMongoDatabase serviceDatabaseDatabase) 
            : base(serviceDatabaseDatabase)
        {
        }
    }
}
