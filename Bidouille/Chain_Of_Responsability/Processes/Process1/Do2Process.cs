namespace Bidouille.Chain_Of_Responsability.Processes.Process1
{
    public class Do2Process : AbstractProcess
    {
        public override IProcess NextProcess { get; set; }

        public Do2Process()
        {
            SetNext(new Do3Process());
        }

        public override IProcess Handle(object? obj)
        {
            Console.WriteLine("do 2");
            return NextProcess;
        }
    }
}