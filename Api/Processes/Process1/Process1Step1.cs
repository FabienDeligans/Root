using Api.Services.MongoDb;
using Library.Processes;
using Library.Processes.Models;

namespace Api.Processes.Process1
{
    public class Process1Step1 : AbstractProcessStep
    {
        public Process1Step1(Process1Step2 process1Step2, ServiceMongoDatabase serviceMongoDatabase) : base(serviceMongoDatabase)
        {
            SetCurrentStep(Process1AllSteps.Process1Step1); 
            SetNext(process1Step2);
        }

        public override void Run(Process? processToUpdate)
        {
            //DO
            Console.WriteLine($"Process1 step1");
        }
    }
}
