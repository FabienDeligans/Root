using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step3Process : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Step3Process()
        {
            SetNext(new StepEndProcess()); 
        }
        public override IProcess RunStep(object? obj)
        {
            // Do something
            return NextProcess;
        }
    }
}
