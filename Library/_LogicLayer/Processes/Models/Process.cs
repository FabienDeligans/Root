using Library._Providers.Models;

namespace Library._LogicLayer.Processes.Models
{
    public class Process : Entity
    {
        public ProcessType ProcessType { get; set; }
        public string? CurrentProcessStep { get; set; }
        public ProcessState ProcessState { get; set; }
    }
}
