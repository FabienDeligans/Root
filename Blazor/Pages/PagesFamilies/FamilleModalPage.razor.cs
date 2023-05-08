using Blazor.Provider;
using Blazored.Modal;
using Blazored.Modal.Services;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.PagesFamilies
{
    public partial class FamilleModalPage
    {
        [Inject]
        public FamilyProvider? FamilyProvider { get; set; }

        [Parameter]
        public string? FamilyId { get; set; }
        private Family Family { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance? BlazoredModal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(FamilyId))
            {
                Family = new Family();
            }
            else
            {
                Family = await FamilyProvider.GetOneSimpleAsync(FamilyId); 
            }
        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(FamilyId))
            {
                Family = await FamilyProvider!.CreateAsync(Family).ConfigureAwait(false);
            }
            else
            {
                Family = await FamilyProvider.UpdateAsync(Family);
            }
            await InvokeAsync(async () => await BlazoredModal!.CloseAsync(ModalResult.Ok(Family)).ConfigureAwait(false));
        }

        private async Task Reset()
        {
            await InvokeAsync(async () => await BlazoredModal!.CancelAsync().ConfigureAwait(false));
        }
    }
}
