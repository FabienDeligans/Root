using _Providers.DatabaseProviders.MongoDb;
using Library._LogicLayer.Logic;
using Library._Providers.Models.Business;

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
