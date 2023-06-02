using Library.Models;

namespace Library.Processes.Models
{
    public class Process : Entity
    {
        public ProcessType ProcessType { get; set; }
        public string? CurrentProcessStep { get; set;  }
        public ProcessState ProcessState { get; set; }
    }

    public enum ProcessState
    {
        Queued = 0, 
        Success = 1, 
        Fail = 2,
    }

    public enum ProcessType
    {
        Process1 = 1,
        Process2 = 2,
    }
}
