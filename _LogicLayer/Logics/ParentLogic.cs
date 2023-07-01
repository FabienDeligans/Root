using _Providers.DatabaseProviders.MongoDb;
using Common.Models.Business;

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
