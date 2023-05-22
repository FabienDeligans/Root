using Library.Models.Process;
using Library.Process;

namespace Api.Processes.Process_1
{
    public class ClientProcess_1
    {
        private readonly IProcess<ProcessType> _step0Process;
        private readonly IProcess<ProcessType> _step1Process;
        private readonly IProcess<ProcessType> _step2Process;
        private readonly IProcess<ProcessType> _step3Process;
        private readonly IProcess<ProcessType> _stepEndProcess; 

        public ClientProcess_1(
            Step0Process step0Process, 
            Step1Process step1Process,
            Step2Process step2Process,
            Step3Process step3Process,
            StepEndProcess stepEndProcess)
        {
            _step0Process = step0Process;
            _step1Process = step1Process;
            _step2Process = step2Process; 
            _step3Process = step3Process; 
            _stepEndProcess = stepEndProcess;
        }

        public void RunProcess()
        {
            IProcess<ProcessType> process = _step0Process;

            while (process != null)
            {
                process = process.RunStep(null); 
            }
        }
    }
}
