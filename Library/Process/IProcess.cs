using Library.Models.Process;

namespace Library.Process
{
    public interface IProcess<T> where T : Enum
    {
        ProcessType ProcessType { get; }
        Task RunProcess(object? obj); 
    }
}
