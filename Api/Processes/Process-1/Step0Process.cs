using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step0Process: AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Step0Process()
        {
            SetNext(new Step1Process()); 
        }
        public override IProcess Handle(object? obj)
        {
            // Do something
            return NextProcess; 
        }
    }
}
