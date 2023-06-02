using Api.Logics;
using Api.Services.MongoDb;
using Library.Api.ApiDatabaseProvider;
using Library.Api.ApiLogicProvider;
using Library.Processes;
using Library.Processes.Models;

namespace Api.Processes.Process1
{
    public class ClientProcess1 : IObserver<Process>
    {
        private readonly IApiLogic<Process> _processLogic;
        public ProcessType ProcessType { get; set; }

        private readonly IProcessStep? _step1;
        private readonly IProcessStep? _step2;

        public ClientProcess1(
            ProcessLogic processLogic,
            Process1Step1? step1,
            Process1Step2? step2)
        {
            ProcessType = ProcessType.Process1;

            _processLogic = processLogic;

            _step1 = step1;
            _step2 = step2;
        }

        private async Task RunProcess1()
        {
            var processesModels = await _processLogic.GetAllFilteredByPropertyEqualAsync(
                        v => v.ProcessType == ProcessType && v.ProcessState != ProcessState.Success);

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
            if (process.ProcessType == ProcessType)
            {
                if (_processLogic
                    .GetAllFilteredByPropertyEqualAsync(v => v.ProcessType == ProcessType)
                    .Result
                    .Contains(process)) return;

                RunProcess1().ConfigureAwait(false);
            }
        }
    }
}
