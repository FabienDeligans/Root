using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step2Process : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Step2Process()
        {
            SetNext(new Step3Process()); 
        }
        public override IProcess Handle(object? obj)
        {
            // Do something
            return NextProcess;
        }
    }
}
