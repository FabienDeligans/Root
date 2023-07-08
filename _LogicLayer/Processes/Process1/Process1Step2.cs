using _Providers.DatabaseProviders;
using Common.Models.Processes;

namespace _LogicLayer.Processes.Process1
{
    public class Process1Step2 : AbstractProcessStep
    {
        public Process1Step2(
            IApiServiceDatabase serviceMongoDatabase) 
            : base(serviceMongoDatabase)
        {
            SetCurrentStep(_Steps.Step2);
            SetNext(null);
        }

        public override void Run(Process? processToUpdate)
        {
            //DO
            Console.WriteLine($"Process1 step2");
        }
    }
}