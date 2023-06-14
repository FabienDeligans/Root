namespace Common.Models.Processes
{
    public class Process : Entity
    {
        public ProcessType ProcessType { get; set; }
        public string? CurrentStep { get; set; }
        public ProcessState ProcessState { get; set; }
    }
}
