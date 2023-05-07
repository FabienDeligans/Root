using Api.Services.MongoDb;
using Library.Api.ApiLogicProvider;
using Library.Models.Business;

namespace Api.Logics
{
    public class FamilyLogic : BaseApiLogic<Family>
    {
        public FamilyLogic(ServiceMongoDatabase serviceDatabaseDatabase) : base(serviceDatabaseDatabase)
        {
        }
    }
}
