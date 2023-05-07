using Blazor.Controller.Modal;
using Blazor.Provider;
using Blazored.Modal;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.PagesFamilies
{
    public partial class FamillePage
    {
        [Inject]
        public FamilyProvider? FamilyProvider { get; set; }

        [Inject]
        public ModalController ModalController { get; set; }

        private Family? Model { get; set; }
        private IEnumerable<Family>? Families { get; set; } = new List<Family>();

        protected override async Task OnInitializedAsync()
        {
            Model = new Family();
            await GetAllFamilies();
        }


        private async Task GetAllFamilies()
        {
            Families = await FamilyProvider.GetAllAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task AutoUpdate(ChangeEventArgs arg, string propertyName)
        {
            if (arg.Value != null)
            {
                var dico = new Dictionary<string, string>()
                {
                    {propertyName, arg.Value.ToString()}
                };

                Model = await FamilyProvider.UpdatePropertyAsync(Model.Id, dico);
                await GetAllFamilies();
            }
        }

        private async Task Add()
        {
            var result = (Family)await ModalController.ShowModal<FamilleModalPage>("Ajouter une famille");

            var getAllFamiliesTask = GetAllFamilies();

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await ModalController.ShowModalDuration($"Enregistrement OK pour la famille {result.Name}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalDuration($"Annulation effectuée", 1000, Alert.Danger);
                }
            });

            await GetAllFamilies();
        }

        private async Task ConfirmDelete(string modelId)
        {
            var parameter = new ModalParameters()
            {
                {nameof(FamilyDeleteModalConfirm.FamilyId), modelId}
            }; 

            var result = (string)await ModalController.ShowModal<FamilyDeleteModalConfirm>($"Supprimer la famille {Model.Name}", parameter);

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await ModalController.ShowModalDuration($"Effacement effectué", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalDuration($"Effacement annulé", 1000, Alert.Danger);
                }
            });

            await GetAllFamilies();

        }
    }
}
