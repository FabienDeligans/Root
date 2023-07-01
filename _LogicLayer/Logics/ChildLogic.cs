using _Providers.DatabaseProviders.MongoDb;
using Common.Models.Business;

namespace _LogicLayer.Logics
{
    public class ChildLogic : BaseApiLogic<Child>
    {
        public ChildLogic(
            ServiceMongoDatabase serviceDatabaseDatabase) 
            : base(serviceDatabaseDatabase)
        {
        }
    }
}
