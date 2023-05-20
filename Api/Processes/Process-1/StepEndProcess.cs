using Library.Process;

namespace Api.Processes.Process_1
{
    public class StepEndProcess : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public StepEndProcess()
        {
            SetNext(null); 
        }
        public override IProcess Handle(object? obj)
        {
            // Do something
            return NextProcess;
        }
    }
}
