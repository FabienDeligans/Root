using System.Text.Json;
using Api.Logics;
using Library.Events;
using Library.Models.Process;
using Library.Process;

namespace Api.Processes.Process_1
{
    public class ClientProcess_1: IProcess<ProcessType>
    {
        public ProcessType ProcessType { get; }
        private readonly Step0Process _step0Process;
        private readonly Step1Process _step1Process;
        private readonly Step2Process _step2Process;
        private readonly Step3Process _step3Process;
        private readonly StepEndProcess _stepEndProcess;
        private readonly ProcessLogic _processLogic;
        public Handler _handler;

        public ClientProcess_1(
            Step0Process step0Process,
            Step1Process step1Process,
            Step2Process step2Process,
            Step3Process step3Process,
            StepEndProcess stepEndProcess,
            ProcessLogic processLogic)
        {
            ProcessType = ProcessType.MonProcess01;

            _step0Process = step0Process;
            _step1Process = step1Process;
            _step2Process = step2Process;
            _step3Process = step3Process;
            _stepEndProcess = stepEndProcess;
            _processLogic = processLogic;
        }

        public void SetHandler(Handler handler)
        {
            _handler = handler;
            _handler.SendEvent += OnSendEvent; 
        }

        private void OnSendEvent(object? sender, ICustomEvent e)
        {
            var myCustomEvent = (MyCustomEvent) e;
            if (myCustomEvent.ValueEvent == ProcessType.MonProcess01.ToString())
            {
                RunProcess(null); 
            }
        }

        public async Task RunProcess(object? obj)
        {
            IProcessStep<ProcessType>? process = null;
            var processesModels = new List<ProcessModel>();

            var processesFailed = await _processLogic
                .GetAllFilteredByPropertyEqualAsync(v =>
                    v.ProcessType == ProcessType.MonProcess01 &&
                    v.ProcessState == ProcessState.Failure);

            if (processesFailed.Any())
            {
                foreach (var processFailed in processesFailed)
                {
                    if (processFailed.CurrentStep.Equals(MyCustomProcessStep.Step0.ToString()))
                    {
                        process = _step0Process;
                    }
                    else if (processFailed.CurrentStep.Equals(MyCustomProcessStep.Step1.ToString()))
                    {
                        process = _step1Process;
                    }
                    else if (processFailed.CurrentStep.Equals(MyCustomProcessStep.Step2.ToString()))
                    {
                        process = _step2Process;
                    }
                    else if (processFailed.CurrentStep.Equals(MyCustomProcessStep.Step3.ToString()))
                    {
                        process = _step3Process;
                    }
                    else if (processFailed.CurrentStep.Equals(MyCustomProcessStep.StepEnd.ToString()))
                    {
                        process = _stepEndProcess;
                    }

                    processesModels.Add(processFailed);
                }
            }
            else
            {
                process = _step0Process;
                var processModel = await _processLogic.CreateAsync(new ProcessModel()
                {
                    ProcessType = ProcessType.MonProcess01,
                    Payload = JsonSerializer.Serialize(obj),
                });
                processesModels.Add(processModel);
            }

            foreach (var processesModel in processesModels)
            {
                while (process != null)
                {
                    process = process.RunStep(processesModel, obj);
                }
            }
        }
    }
}
