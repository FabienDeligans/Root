using Api.Services.MongoDb;
using Library.Api.ApiLogicProvider;
using Library.Models.Process;

namespace Api.Logics
{
    public class ProcessLogic : BaseApiLogic<ProcessModel>
    {
        public ProcessLogic(ServiceMongoDatabase serviceDatabase) : base(serviceDatabase)
        {
        }
    }
}
