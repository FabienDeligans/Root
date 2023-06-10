using _Providers.DatabaseProviders.MongoDb;
using Library._LogicLayer.Logic;
using Library._LogicLayer.Processes.Models;

namespace _LogicLayer.Logics;

public class ProcessLogic : BaseApiLogic<Process>
{
    public ProcessLogic(
        ServiceMongoDatabase serviceDatabaseDatabase) 
        : base(serviceDatabaseDatabase)
    {
    }
}