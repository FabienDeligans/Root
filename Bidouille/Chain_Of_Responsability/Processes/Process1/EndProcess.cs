namespace Bidouille.Chain_Of_Responsability.Processes.Process1
{
    public class EndProcess : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public EndProcess()
        {
            SetNext(null);
        }

        public override IProcess Handle(object? obj)
        {
            Console.WriteLine("End");
            return null;
        }
    }
}