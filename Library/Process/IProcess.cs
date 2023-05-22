using Library.Models.Process;

namespace Library.Process
{
    public interface IProcess<T> where T : Enum
    {
        public IProcess<T> NextProcess { get; set; }
        public IProcess<T> SetNext(IProcess<T> nextProcess);
        public IProcess<T> RunStep(ProcessModel processModel, object? obj);
    }
}
