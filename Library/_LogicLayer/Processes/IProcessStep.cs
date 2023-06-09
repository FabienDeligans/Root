using Library._LogicLayer.Processes.Models;

namespace Library._LogicLayer.Processes
{
    public interface IProcessStep
    {
        public IProcessStep? NextProcess { get; set; }
        public IProcessStep? SetNext(IProcessStep nextProcess);
        public IProcessStep? Handle(Process? processToUpdate);
    }
}
