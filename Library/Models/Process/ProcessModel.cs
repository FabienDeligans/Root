namespace Library.Models.Process
{
    public class ProcessModel : Entity
    {
        public ProcessType ProcessType { get; set; }
        public string CurrentStep { get; set; }
        public ProcessState ProcessState { get; set; }
        public string Payload { get; set; }
    }
}
