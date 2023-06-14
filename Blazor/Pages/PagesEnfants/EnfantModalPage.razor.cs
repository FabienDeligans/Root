using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Blazor.Provider.Api;
using Blazored.Modal;
using Common.Models.Business;

namespace Blazor.Pages.PagesEnfants
{
    public partial class EnfantModalPage
    {
        [Inject]
        FamilyProvider? FamilyProvider { get; set; }

        [Inject]
        ChildProvider? ChildProvider { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance? BlazoredModalInstance { get; set; }

        [Parameter]
        public string ModelId { get; set; }

        private IEnumerable<Family>? Families { get; set; }

        private Child? Child { get; set; }
        private bool IsDisabled { get; set; }

        private string _statusMessage;
        private string _statusClass;

        protected override async Task OnInitializedAsync()
        {
            Families = await FamilyProvider!.GetAllAsync();

            if (ModelId != null)
            {
                Child = await ChildProvider!.GetOneSimpleAsync(ModelId);
                IsDisabled = true;
            }
            else
            {
                Child = new Child();
                IsDisabled = false;
            }
        }

        private async Task Submit()
        {
            if (ModelId != null)
            {
                Child = await ChildProvider!.UpdateAsync(Child);
            }
            else
            {
                Child = await ChildProvider!.CreateAsync(Child!);
            }

            await BlazoredModalInstance!.CloseAsync(ModalResult.Ok(Child));
        }

        private async Task Cancel()
        {
            await InvokeAsync(async () => await BlazoredModalInstance!.CancelAsync().ConfigureAwait(false));
        }
    }
}
