using _Providers.DatabaseProviders;
using Common.Models.Processes;
using Process = Common.Models.Processes.Process;

namespace _LogicLayer.Processes
{
    public abstract class AbstractProcessStep : IProcessStep
    {
        public Enum CurrentStep { get; set; }
        public IProcessStep? NextProcess { get; set; }

        private readonly IApiServiceDatabase _serviceDatabase;

        protected AbstractProcessStep(IApiServiceDatabase serviceDatabase)
        {
            _serviceDatabase = serviceDatabase;
        }

        public IProcessStep? SetNext(IProcessStep? nextProcess)
        {
            if (nextProcess == null) return null;
            NextProcess = nextProcess;
            return NextProcess;
        }

        public IProcessStep SetCurrentStep(Enum currentStep)
        {
            CurrentStep = currentStep;
            return this;
        }

        public IProcessStep? Handle(Process? processToUpdate)
        {
            try
            {
                if (processToUpdate != null)
                {
                    Run(processToUpdate);

                    processToUpdate.ProcessState = ProcessState.Success;
                    processToUpdate.CurrentStep = CurrentStep;
                    processToUpdate.UpdateDate = DateTime.Now;
                   
                    _serviceDatabase.UpdateAsync(processToUpdate);
                    return NextProcess;
                }

                return null;
            }
            catch (Exception e)
            {
                if (processToUpdate != null)
                {
                    processToUpdate.ProcessState = ProcessState.Fail;
                    processToUpdate.CurrentStep = CurrentStep;
                    processToUpdate.UpdateDate = DateTime.Now;

                    _serviceDatabase.UpdateAsync(processToUpdate);
                }
                throw; 
            }

        }

        public abstract void Run(Process? processToUpdate);
    }
}
