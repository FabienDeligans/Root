namespace Library.Process
{
    public interface IProcess<T> where T : Enum
    {
        Task RunProcess(object? obj); 
    }
}
