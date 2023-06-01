using Api.Services.MongoDb;
using Library.Api.ApiLogicProvider;
using Library.Processes.Models;

namespace Api.Logics;

public class ProcessLogic : BaseApiLogic<Process>
{
    public ProcessLogic(ServiceMongoDatabase serviceDatabaseDatabase) : base(serviceDatabaseDatabase)
    {
    }
}