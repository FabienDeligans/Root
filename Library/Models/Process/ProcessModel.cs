namespace Library.Models.Process
{
    public class ProcessModel : Entity
    {
        public ProcessType ProcessType { get; set; }
        public string ProcessName { get; set; }
        public Enum CurrentStep { get; set; }
        public ProcessState ProcessState { get; set; }
    }
}
