using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class Production
    {
        [Parameter]
        public string GammeId { get; set; }

        [Parameter]
        public string OfId { get; set; }

        [Inject]
        private ICallApi<OrdreFabrication> OrdreFabricationProvider { get; set; }

        public Etape EtapeToDisplay { get; set; }

        public OrdreFabrication OrdreFabrication { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(GammeId))
            {
                OrdreFabrication = new OrdreFabrication()
                {
                    GammeId = GammeId
                };
                OrdreFabrication = await OrdreFabricationProvider.CreateAsync(OrdreFabrication);
            }

            if (!string.IsNullOrWhiteSpace(OfId))
            {
                OrdreFabrication = await OrdreFabricationProvider.GetOneFullAsync(OfId); 
            }
        }

        private async Task DisplayStep(Etape step)
        {
            EtapeToDisplay = step;
            await InvokeAsync(StateHasChanged);
        }

        private async Task EndStep(Etape etapeToDisplay)
        {
            OrdreFabrication.EtapesExecuted.Add(etapeToDisplay);

            OrdreFabrication = await OrdreFabricationProvider.UpdateAsync(OrdreFabrication);

            await InvokeAsync(StateHasChanged);
        }
    }
}
