using Blazored.Modal.Services;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;
using Blazor.Provider;
using Blazored.Modal;

namespace Blazor.Pages.PagesParents
{
    public partial class ParentModalPage
    {
        [Inject]
        public FamilyProvider? FamilyProvider { get; set; }

        [Inject]
        public ParentProvider? ParentProvider { get; set; }

        private IEnumerable<Family>? Families { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance? BlazoredModal { get; set; }

        [Parameter]
        public string? ModelId { get; set; }

        private Parent? Parent { get; set; }

        private string? _statusMessage;
        private string? _statusClass;
        private bool IsDisabled { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Families = await FamilyProvider!.GetAllAsync();

            if (ModelId != null)
            {
                Parent = await ParentProvider!.GetOneFullAsync(ModelId);
                IsDisabled = true;
            }
            else
            {
                Parent = new Parent();
                IsDisabled = false;
            }
        }

        private async Task Submit()
        {
            if (ModelId != null)
            {
                Parent = await ParentProvider!.UpdateAsync(Parent);
            }
            else
            {
                Parent = await ParentProvider!.CreateAsync(Parent!);
            }

            await BlazoredModal!.CloseAsync(ModalResult.Ok(Parent));
        }

        private async Task Cancel()
        {
            await InvokeAsync(async () => await BlazoredModal!.CancelAsync().ConfigureAwait(false));
        }
    }
}
