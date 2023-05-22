using Api.Logics;
using Library.Models.Process;
using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step3Process : AbstractProcess<ProcessType>
    {
        public override IProcess<ProcessType> NextProcess { get; set; }
        public override ProcessType ProcessType { get; set; }
        public override ProcessState ProcessState { get; set; }
        public override Enum CurrentStep { get; set; }
        public override Enum NextStep { get; set; }

        public Step3Process(StepEndProcess next, ProcessLogic processLogic) : base(processLogic)
        {
            ProcessType = ProcessType.MonProcess01;
            ProcessState = ProcessState.Processing;
            CurrentStep = MyCustomProcessStep.Step3;
            NextStep = MyCustomProcessStep.StepEnd;

            SetNext(next);
        }

        protected override void Run()
        {
            //throw new NotImplementedException();
        }
    }
}
