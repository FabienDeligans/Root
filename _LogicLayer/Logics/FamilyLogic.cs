using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.Business;

namespace _LogicLayer.Logics
{
    public class FamilyLogic : BaseApiLogic<Family>
    {
        private readonly ILogic<Parent> _parentLogic;
        private readonly ILogic<Child> _childLogic;
        public FamilyLogic(
            IApiServiceDatabase serviceDatabase,
            ILogic<Parent> parentLogic,
            ILogic<Child> childLogic) 
            : base(serviceDatabase)
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
