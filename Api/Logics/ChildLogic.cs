using Api.Services.MongoDb;
using Library.Abstract;
using Library.Models.Business;

namespace Api.Logics
{
    public class ChildLogic : BaseApiLogic<Child>
    {
        public ChildLogic(ServiceMongoDatabase serviceDatabaseDatabase) : base(serviceDatabaseDatabase)
        {
        }
    }
}
