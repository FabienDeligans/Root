using Blazor.Controller.Modal;
using Blazor.Provider;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.PagesParents
{
    public partial class ParentPage
    {
        [Inject]
        public ParentProvider? ParentProvider { get; set; }

        [Inject]
        public ModalController ModalController { get; set; }

        private Parent? Model { get; set; }
        private IEnumerable<Parent>? Parents { get; set; } = new List<Parent>();

        protected override async Task OnInitializedAsync()
        {
            Model = new Parent();
            await GetAllParents();
        }


        private async Task GetAllParents()
        {
            Parents = await ParentProvider.GetAllAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task AutoUpdate(ChangeEventArgs arg, string propertyName)
        {
            if (arg.Value != null)
            {
                var dico = new Dictionary<string, object>()
                {
                    {propertyName, arg.Value.ToString()}
                };

                Model = await ParentProvider.UpdatePropertyAsync(Model.Id, dico);
                await GetAllParents();
            }
        }

        //private async Task Add()
        //{
        //    var result = (Parent)await ModalController.ShowModal<XXXXXX>("Ajouter une famille");

        //    var getAllParentsTask = GetAllParents();

        //    await InvokeAsync(async () =>
        //    {
        //        if (result != null)
        //        {
        //            await ModalController.ShowModalAlert($"Enregistrement OK pour le parent {result.LastName}", 1000, Alert.Success);
        //        }
        //        else
        //        {
        //            await ModalController.ShowModalAlert($"Annulation effectuée", 1000, Alert.Danger);
        //        }
        //    });

        //    await GetAllParents();
        //}

        //private async Task ConfirmDelete(string modelId)
        //{
        //    var parameter = new ModalParameters()
        //    {
        //        {nameof(XXXXXXXXXXX.ParentId), modelId}
        //    };

        //    var result = (string)await ModalController.ShowModal<XXXXXXXXX>($"Supprimer le parent {Model.LastName}", parameter);

        //    await InvokeAsync(async () =>
        //    {
        //        if (result != null)
        //        {
        //            await ModalController.ShowModalAlert($"Effacement effectué", 1000, Alert.Success);
        //        }
        //        else
        //        {
        //            await ModalController.ShowModalAlert($"Effacement annulé", 1000, Alert.Danger);
        //        }
        //    });

        //    await GetAllParents();

        //}
    }
}
