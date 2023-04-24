using Api.Services;
using Library.Abstract;
using Library.Models.Business;

namespace Api.Logics
{
    public class ChildLogic : BaseApiLogic<Child>
    {
        public ChildLogic(ServiceDatabase serviceDatabase) : base(serviceDatabase)
        {
        }
    }
}
