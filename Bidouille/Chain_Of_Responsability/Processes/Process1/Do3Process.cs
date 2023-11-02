namespace Bidouille.Chain_Of_Responsability.Processes.Process1
{
    public class Do3Process : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Do3Process()
        {
            SetNext(new EndProcess());
        }

        public override IProcess Handle(object? obj)
        {
            Console.WriteLine("do 3");
            return NextProcess;
        }
    }
}