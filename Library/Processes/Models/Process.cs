using Library.Models;

namespace Library.Processes.Models
{
    public class Process : Entity
    {
        public ProcessType ProcessType { get; set; }
        public Enum CurrentProcessStep { get; set;  }
        public ProcessState ProcessState { get; set; }
    }

    public enum ProcessState
    {
        Fail = 0,
        Success = 1, 
    }

    public enum ProcessType
    {
        Process1 = 1,
        Process2 = 2,
    }
}
