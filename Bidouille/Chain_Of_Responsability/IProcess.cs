namespace Bidouille.Chain_Of_Responsability
{
    public interface IProcess
    {
        public IProcess NextProcess { get; set; }
        public IProcess SetNext(IProcess nextProcess); 
        public IProcess Handle(object? obj);
    }
}
