using Library._Providers.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Library._LogicLayer.Processes.Models
{
    public class Process : Entity
    {
        public ProcessType ProcessType { get; set; }
        public string? CurrentStep { get; set; }
        public ProcessState ProcessState { get; set; }
    }
}
