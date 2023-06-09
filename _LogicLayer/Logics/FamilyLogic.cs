using _LogicLayer.Processes;
using _Providers.DatabaseProviders.MongoDb;
using Library._LogicLayer.Logic;
using Library._LogicLayer.Processes.Models;
using Library._Providers.DatabaseProvider;
using Library._Providers.Models.Business;

namespace _LogicLayer.Logics
{
    public class FamilyLogic : BaseApiLogic<Family>
    {
        private readonly ParentLogic _parentLogic;
        private readonly ChildLogic _childLogic;
        private readonly ProcessHandler _processHandler;
        public FamilyLogic(
            ProcessHandler processHandler,
            ServiceMongoDatabase serviceDatabaseDatabase,
            ParentLogic parentLogic,
            ChildLogic childLogic) 
            : base(serviceDatabaseDatabase)
        {
            _processHandler = processHandler;
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
