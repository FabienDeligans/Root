namespace Common.Models.Processes
{
    public class Process : Entity
    {
        public ProcessType ProcessType { get; set; }
        public Enum? CurrentStep { get; set; }
        public ProcessState ProcessState { get; set; }
    }
}
