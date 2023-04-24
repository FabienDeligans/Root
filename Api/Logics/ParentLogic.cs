using Api.Services;
using Library.Abstract;
using Library.Models.Business;

namespace Api.Logics
{
    public class ParentLogic : BaseApiLogic<Parent>
    {
        public ParentLogic(ServiceDatabase serviceDatabase) : base(serviceDatabase)
        {
        }
    }
}
