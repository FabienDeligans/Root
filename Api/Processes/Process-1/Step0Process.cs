using Library.Models.Process;
using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step0Process : AbstractProcess<ProcessType>
    {
        public override IProcess<ProcessType> NextProcess { get; set; }
        public override ProcessType ProcessType { get; set; }
        public override ProcessState ProcessState { get; set; }
        public override Enum CurrentStep { get; set; }
        public override Enum NextStep { get; set; }

        public Step0Process(Step1Process next)
        {
            ProcessType = ProcessType.MonProcess01;
            ProcessState = ProcessState.Processing; 
            CurrentStep = MyCustomProcessStep.Step0;
            NextStep = MyCustomProcessStep.Step1;

            SetNext(next); 
        }

        protected override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
