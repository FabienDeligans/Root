using Blazor.Controller.Modal;
using Blazor.Provider;
using Blazored.Modal;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class FamillePage
    {
        [Inject]
        public FamilyProvider? FamilyProvider { get; set; }

        private Family Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Model = new Family();
            await GetAllFamilies();
        }




        private async Task Save()
        {
            Model = await FamilyProvider!.CreateAsync(Model).ConfigureAwait(false);
            await GetAllFamilies();
            await InvokeAsync(StateHasChanged);
        }

        private async Task Reset()
        {
            Model = new Family();
            await InvokeAsync(StateHasChanged);
        }

        private IEnumerable<Family> Families { get; set; } = new List<Family>();

        private async Task GetAllFamilies()
        {
            Families = await FamilyProvider.GetAllAsync().ConfigureAwait(false);
            await InvokeAsync(StateHasChanged);
        }

        private async Task AutoUpdate(ChangeEventArgs arg, string propertyName)
        {
            var dico = new Dictionary<string, string>()
            {
                {propertyName, arg.Value.ToString()}
            };
            Model = await FamilyProvider.UpdatePropertyAsync(Model.Id, dico);
            await GetAllFamilies();
            await InvokeAsync(StateHasChanged);

        }

    }
}
