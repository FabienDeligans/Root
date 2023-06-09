using _LogicLayer.Logics;
using Library._LogicLayer.Logic;
using Library._LogicLayer.Processes;
using Library._LogicLayer.Processes.Models;

namespace _LogicLayer.Processes.Process1
{
    public class ClientProcess1 : IObserver<Process>
    {
        private readonly ILogic<Process> _processLogic;
        private readonly ProcessType _processType;

        private readonly IProcessStep? _step1;
        private readonly IProcessStep? _step2;

        public ClientProcess1(
            ProcessLogic processLogic,
            Process1Step1? step1,
            Process1Step2? step2)
        {
            _processType = ProcessType.Process1;
            _processLogic = processLogic;

            _step1 = step1;
            _step2 = step2;
        }

        private async Task RunProcess1()
        {
            var processesModels = await _processLogic.GetAllFilteredByPropertyEqualAsync(
                        v => v.ProcessType == _processType && v.ProcessState != ProcessState.Success);

            foreach (var processModel in processesModels)
            {
                IProcessStep? step = null;

                if (processModel.CurrentProcessStep == Process1AllSteps.Process1Step1.ToString()
                    || processModel.CurrentProcessStep == null)
                {
                    step = _step1;
                }
                if (processModel.CurrentProcessStep == Process1AllSteps.Process1Step2.ToString())
                {
                    step = _step2;
                }

                while (step != null)
                {
                    step = step.Handle(processModel);
                }
            }
        }

        private IDisposable _disposableSubscription;

        public void Subscribe(ProcessHandler subscription)
        {
            _disposableSubscription = subscription.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Process process)
        {
            if (process.ProcessType == _processType)
            {
                if (_processLogic
                    .GetAllFilteredByPropertyEqualAsync(v => v.ProcessType == _processType)
                    .Result
                    .Contains(process)) return;

                RunProcess1().ConfigureAwait(false);
            }
        }
    }
}
