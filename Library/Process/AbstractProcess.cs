namespace Library.Process
{
    public abstract class AbstractProcess : IProcess
    {
        public abstract IProcess NextProcess { get; set; }
        public IProcess SetNext(IProcess nextProcess)
        {
            NextProcess = nextProcess;
            return nextProcess;
        }

        public abstract IProcess RunStep(object? obj);
    }
}
