using _LogicLayer.Logics;
using Back._LogicLayer.Logic;
using Back._LogicLayer.Processes;
using Common.Models.Processes;

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
            try
            {
                var processesModels = await _processLogic.GetAllFilteredByPropertyEqualAsync(
                               v => v.ProcessType == _processType && v.ProcessState != ProcessState.Success);
                
                foreach (var processModel in processesModels.ToList())
                {
                    IProcessStep? step = null;

                    switch (processModel.CurrentStep)
                    {
                        case null:
                        case _Steps.Step1:
                            step = _step1;
                            break;
                        case _Steps.Step2:
                            step = _step2;
                            break;
                    }

                    while (step != null)
                    {
                        step = step.Handle(processModel);
                    }
                }
            }
            catch (Exception e)
            {
                var msg = e.Message;

                OnError(e);

                throw;
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

        private int _errorCount { get; set; } = 0; 
        public void OnError(Exception error)
        {
            _errorCount++;
            if (_errorCount < 10)
            {
                RunProcess1(); 
            }
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
