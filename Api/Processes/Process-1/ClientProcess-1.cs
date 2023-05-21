using Library.Process;

namespace Api.Processes.Process_1
{
    public class ClientProcess_1
    {
        public void RunProcess()
        {
            IProcess process = null;

            while (process != null)
            {
                process = process.RunStep(null); 
            }
        }
    }
}
