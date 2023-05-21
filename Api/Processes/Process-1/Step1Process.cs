using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step1Process : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Step1Process()
        {
            SetNext(new Step2Process()); 
        }
        public override IProcess RunStep(object? obj)
        {
            // Do something
            return NextProcess;
        }
    }
}
