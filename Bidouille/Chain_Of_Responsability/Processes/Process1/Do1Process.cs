namespace Bidouille.Chain_Of_Responsability.Processes.Process1
{
    public class Do1Process : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Do1Process()
        {
            SetNext(new Do2Process());
        }

        public override IProcess Handle(object? obj)
        {
            Console.WriteLine("do 1");
            return NextProcess;
        }
    }
}