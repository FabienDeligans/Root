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

        private Family Model { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance? BlazoredModal { get; set; }

        protected override void OnInitialized()
        {
            Model = new Family();
        }

        private async Task Save()
        {
            Model = await FamilyProvider!.CreateAsync(Model).ConfigureAwait(false);
            await InvokeAsync(async () => await BlazoredModal!.CloseAsync(ModalResult.Ok(Model)).ConfigureAwait(false));
        }

        private async Task Reset()
        {
            await InvokeAsync(async () => await BlazoredModal!.CancelAsync().ConfigureAwait(false));
        }
    }
}
