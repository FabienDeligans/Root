using Common.Models.Processes;

namespace Back._LogicLayer.Processes
{
    public interface IProcessStep
    {
        public IProcessStep? NextProcess { get; set; }
        public IProcessStep? SetNext(IProcessStep nextProcess);
        public IProcessStep? Handle(Process? processToUpdate);
    }
}
