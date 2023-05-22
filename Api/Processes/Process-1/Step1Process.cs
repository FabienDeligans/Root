using Api.Logics;
using Library.Models.Process;
using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step1Process : AbstractProcess<ProcessType>
    {
        public override IProcess<ProcessType> NextProcess { get; set; }
        public override ProcessType ProcessType { get; set; }
        public override ProcessState ProcessState { get; set; }
        public override Enum CurrentStep { get; set; }
        public override Enum NextStep { get; set; }

        public Step1Process(Step2Process next, ProcessLogic processLogic) : base(processLogic)
        {
            ProcessType = ProcessType.MonProcess01;
            ProcessState = ProcessState.Processing;
            CurrentStep = MyCustomProcessStep.Step1;
            NextStep = MyCustomProcessStep.Step2;

            SetNext(next); 
        }

        protected override void Run()
        {
            //throw new NotImplementedException();
        }
    }
}
