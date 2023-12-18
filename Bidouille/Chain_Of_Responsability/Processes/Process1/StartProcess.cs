namespace Bidouille.Chain_Of_Responsability.Processes.Process1
{
    public class StartProcess : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public StartProcess()
        {
            SetNext(new Do1Process());
        }

        public override IProcess Handle(object? obj)
        {
            Console.WriteLine("Start");
            return NextProcess;
        }
    }
}
