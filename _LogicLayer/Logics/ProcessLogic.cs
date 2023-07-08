using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.Processes;

namespace _LogicLayer.Logics;

public class ProcessLogic : BaseApiLogic<Process>
{
    public ProcessLogic(
        IApiServiceDatabase serviceDatabase) 
        : base(serviceDatabase)
    {
    }
}