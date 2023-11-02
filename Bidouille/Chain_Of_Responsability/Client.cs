namespace Bidouille.Chain_Of_Responsability
{
    public class Client
    {
        public void ClientProcess(IProcess process)
        {
            while (process != null)
            {
                process = process.Handle(null); 
            }
        }
    }
}
