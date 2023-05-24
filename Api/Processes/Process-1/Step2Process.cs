using Api.Logics;
using Library.Models.Process;
using Library.Process;

namespace Api.Processes.Process_1
{
    public class Step2Process : AbstractProcess<ProcessType>
    {
        public override IProcessStep<ProcessType> NextProcess { get; set; }
        public override ProcessType ProcessType { get; set; }
        public override ProcessState ProcessState { get; set; }
        public override Enum CurrentStep { get; set; }
        public override Enum NextStep { get; set; }

        public Step2Process(Step3Process next, ProcessLogic processLogic) : base(processLogic)
        {
            ProcessType = ProcessType.MonProcess01;
            ProcessState = ProcessState.Processing;
            CurrentStep = MyCustomProcessStep.Step2;
            NextStep = MyCustomProcessStep.Step3;

            SetNext(next);
        }
        
        protected override void Run()
        {
            Console.WriteLine($"{ProcessType} {CurrentStep}");
            //throw new NotImplementedException();
        }
    }
}
