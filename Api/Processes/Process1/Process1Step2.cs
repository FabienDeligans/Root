using Api.Services.MongoDb;
using Library.Processes;
using Library.Processes.Models;

namespace Api.Processes.Process1
{
    public class Process1Step2 : AbstractProcessStep
    {
        public Process1Step2(ServiceMongoDatabase serviceMongoDatabase) : base(serviceMongoDatabase)
        {
            SetCurrentStep(Process1AllSteps.Process1Step2);
            SetNext(null);
        }

        public override void Run(Process? processToUpdate)
        {
            //DO
        }
    }
}