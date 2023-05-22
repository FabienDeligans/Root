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

        public IProcess<T> SetNext(IProcess<T>? nextProcess)
        {
            if (nextProcess == null)
            {
                return null; 
            }

            NextProcess = nextProcess;
            return nextProcess;
        }

        public abstract IProcess<T> RunStep(object? obj);
    }
}
