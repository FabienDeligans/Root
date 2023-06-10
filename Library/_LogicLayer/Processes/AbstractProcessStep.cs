﻿using Library._LogicLayer.Processes.Models;
using Library._Providers.DatabaseProvider;
using Process = Library._LogicLayer.Processes.Models.Process;

namespace Library._LogicLayer.Processes
{
    public abstract class AbstractProcessStep : IProcessStep
    {
        public string CurrentStep { get; set; }
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

        public IProcessStep SetCurrentStep(string currentStep)
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
                    processToUpdate.CurrentProcessStep = CurrentStep;
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
                    processToUpdate.CurrentProcessStep = CurrentStep;
                    processToUpdate.UpdateDate = DateTime.Now;

                    _serviceDatabase.UpdateAsync(processToUpdate);
                }
                return null;
            }

        }

        public abstract void Run(Process? processToUpdate);
    }
}
