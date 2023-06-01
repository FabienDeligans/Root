using Library.Api.ApiDatabaseProvider;
using Library.Processes.Models;
using Process = Library.Processes.Models.Process;

namespace Library.Processes
{
    public abstract class AbstractProcessStep : IProcessStep
    {
        public Enum CurrentProcess { get; set; }
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
            CurrentProcess = currentStep;
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
                    processToUpdate.CurrentProcessStep = CurrentProcess;
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
                    processToUpdate.CurrentProcessStep = CurrentProcess;
                    processToUpdate.UpdateDate = DateTime.Now;

                    _serviceDatabase.UpdateAsync(processToUpdate); 
                }
                return null;
            }

        }

        public abstract void Run(Process? processToUpdate);
    }
}
