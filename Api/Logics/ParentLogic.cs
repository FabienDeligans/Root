using Api.Services.MongoDb;
using Library.Api.ApiLogicProvider;
using Library.Models.Business;

namespace Api.Logics
{
    public class ParentLogic : BaseApiLogic<Parent>
    {
        public ParentLogic(ServiceMongoDatabase serviceDatabaseDatabase) : base(serviceDatabaseDatabase)
        {
        }
    }
}
