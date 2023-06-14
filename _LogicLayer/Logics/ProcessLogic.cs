using _Providers.DatabaseProviders.MongoDb;
using Back._LogicLayer.Logic;
using Common.Models.Processes;

namespace _LogicLayer.Logics;

public class ProcessLogic : BaseApiLogic<Process>
{
    public ProcessLogic(
        ServiceMongoDatabase serviceDatabaseDatabase) 
        : base(serviceDatabaseDatabase)
    {
    }
}