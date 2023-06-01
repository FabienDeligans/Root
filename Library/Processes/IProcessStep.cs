using Library.Processes.Models;

namespace Library.Processes
{
    public interface IProcessStep
    {
        public IProcessStep? NextProcess { get; set; }
        public IProcessStep? SetNext(IProcessStep nextProcess);
        public IProcessStep? Handle(Process? processToUpdate);
    }
}
