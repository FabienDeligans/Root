using System.Text.Json;
using Library.Api.ApiLogicProvider;
using Library.Models.Process;

namespace Library.Process
{
    public abstract class AbstractProcess<T> : IProcess<T> where T : Enum
    {
        public abstract IProcess<T> NextProcess { get; set; }
        public abstract ProcessType ProcessType { get; set; }
        public abstract ProcessState ProcessState { get; set; }
        public abstract Enum CurrentStep { get; set; }
        public abstract Enum NextStep { get; set; }

        public readonly IApiLogic<ProcessModel> _proccesLogic; 
        public AbstractProcess(IApiLogic<ProcessModel> processLogic)
        {
            _proccesLogic = processLogic;
        }

        public IProcess<T> SetNext(IProcess<T>? nextProcess)
        {
            if (nextProcess == null)
            {
                return null; 
            }

            NextProcess = nextProcess;
            return nextProcess;
        }

        public IProcess<T> RunStep(ProcessModel processModel, object? obj)
        {
            try
            {
                // Do something
                Run(); 

                // update success
                ProcessState = ProcessState.Success;

                processModel.ProcessState = ProcessState;
                processModel.CurrentStep = CurrentStep.ToString();
                processModel.Payload = JsonSerializer.Serialize(obj); 

                _proccesLogic.UpdateAsync(processModel); 

                return NextProcess;
            }
            catch (Exception)
            {
                //update failure
                ProcessState = ProcessState.Failure;

                processModel.ProcessState = ProcessState;
                processModel.CurrentStep = CurrentStep.ToString();
                processModel.Payload = JsonSerializer.Serialize(obj);

                _proccesLogic.UpdateAsync(processModel);

                return null;
            }
        }

        protected abstract void Run();
    }
}
