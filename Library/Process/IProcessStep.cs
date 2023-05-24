using Library.Models.Process;

namespace Library.Process
{
    public interface IProcessStep<T> where T : Enum
    {
        public IProcessStep<T> NextProcess { get; set; }
        public IProcessStep<T> SetNext(IProcessStep<T> nextProcess);
        public IProcessStep<T> RunStep(ProcessModel processModel, object? obj);
    }
}
