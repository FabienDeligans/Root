using Blazor.Pages.Components;
using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class _GammeOfArticleComponent : ChildComponentBase
    {
        [Inject]
        public ICallApi<Gamme>? GammeProvider { get; set; }

        [CascadingParameter]
        public Gamme Gamme { get; set; } = new Gamme();

        protected override async Task OnInitializedAsync()
        {
            if (Gamme.Id != null)
            {
                Gamme = await GammeProvider.GetOneFullAsync(Gamme.Id);
            }
            else
            {
                Gamme = new Gamme(); 
            }
        }

        private async Task SubmitGamme()
        {
            if (Gamme.Id != null)
            {
                Gamme = await GammeProvider.UpdateAsync(Gamme);
            }
            else
            {
                Gamme = await GammeProvider.CreateAsync(Gamme);
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task Cancel()
        {
            Gamme = new Gamme();
            await InvokeAsync(StateHasChanged);
        }
    }
}
