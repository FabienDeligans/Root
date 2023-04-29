using Api.Services.MongoDb;
using Library.Abstract;
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
