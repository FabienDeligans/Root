using Api.Services.MongoDb;
using Library.Api.ApiLogicProvider;
using Library.Models.Business;

namespace Api.Logics
{
    public class FamilyLogic : BaseApiLogic<Family>
    {
        private readonly ParentLogic _parentLogic;
        private readonly ChildLogic _childLogic;
        public FamilyLogic(ServiceMongoDatabase serviceDatabaseDatabase, ParentLogic parentLogic, ChildLogic childLogic) : base(serviceDatabaseDatabase)
        {
            _parentLogic = parentLogic;
            _childLogic = childLogic;
        }

        public override async Task<Family> UpdateAsync(Family entityUpdate)
        {
            var fullFamily = await ServiceDatabase.GetCollectionEntity(entityUpdate);
            foreach (var parent in fullFamily.Parents)
            {
                _parentLogic.UpdatePropertyAsync(parent.Id, new Dictionary<string, object> { { nameof(Parent.IsDisabled), entityUpdate.IsDisabled } });
            }
            foreach (var enfant in fullFamily.Children)
            {
                _childLogic.UpdatePropertyAsync(enfant.Id, new Dictionary<string, object> { { nameof(Child.IsDisabled), entityUpdate.IsDisabled } });
            }

            return await ServiceDatabase.UpdateAsync(entityUpdate);
        }
    }
}
