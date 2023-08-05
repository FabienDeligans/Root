using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class Production
    {
        [Parameter] public string GammeId { get; set; }

        [Parameter] public string OfId { get; set; }

        [Inject] public ICallApi<OrdreFabrication> OrdreFabricationProvider { get; set; }

        [Inject] public ICallApi<Article> ArticleProvider { get; set; }

        public Etape EtapeToDisplay { get; set; }

        public List<OrdreFabrication> OfList { get; set; }

        public OrdreFabrication OrdreFabrication { get; set; }

        public bool AllOfs { get; set; }
        public bool DisplayStartButton { get; set; }
        public bool DisplayEndButton { get; set; }
        public bool DisplayOfEnded { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(OfId))
            {
                OrdreFabrication = await OrdreFabricationProvider.GetOneFullAsync(OfId);
            }

            var ofs = await OrdreFabricationProvider.GetAllAsync();
            OfList = ofs.Where(v => v.IsDisabled == false).ToList();
        }

        private async Task DisplayStep(Etape step)
        {
            EtapeToDisplay = step;
            DisplayInformations();

            await InvokeAsync(StateHasChanged);
        }

        private void DisplayInformations()
        {
            var etapeExecuted = OrdreFabrication.EtapesExecuted.FirstOrDefault(v => v.Id == EtapeToDisplay.Id);

            DisplayStartButton = true;
            DisplayEndButton = false;

            if (etapeExecuted != null)
            {
                if (etapeExecuted.Start == default)
                {
                    DisplayStartButton = true;
                }
                else
                {
                    DisplayStartButton = false;
                }

                if (etapeExecuted.End == default)
                {
                    DisplayEndButton = true;
                }
                else
                {
                    DisplayEndButton = false;
                }
            }

            if (OrdreFabrication.EtapesExecuted.Count == OrdreFabrication.Gamme.Etapes.Count )
            {
                foreach (var executed in OrdreFabrication.EtapesExecuted)
                {
                    if (executed.End == default)
                    {
                        DisplayOfEnded = false;
                    }
                    else
                    {
                        DisplayOfEnded = true; 
                    }
                }
            }
        }

        private async Task LoadOf(ChangeEventArgs arg)
        {
            OfId = null;
            EtapeToDisplay = null;
            OrdreFabrication = null;

            if (!string.IsNullOrWhiteSpace(arg.Value.ToString()))
            {
                OfId = arg.Value.ToString();
                OrdreFabrication = await OrdreFabricationProvider.GetOneFullAsync(OfId);
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task Reload()
        {
            AllOfs = !AllOfs;

            var ofs = await OrdreFabricationProvider.GetAllAsync();
            ofs = AllOfs == true ? ofs : ofs.Where(v => v.IsDisabled == AllOfs);

            OfList = ofs.ToList();

            OfId = null;
            EtapeToDisplay = null;
            OrdreFabrication = null;

            await InvokeAsync(StateHasChanged);
        }

        private async Task StartStep(Etape etapeToDisplay)
        {
            if (!OrdreFabrication.EtapesExecuted.Select(v => v.Id).Contains(etapeToDisplay.Id))
            {
                var executedStep = new EtapeExecuted
                {
                    Id = etapeToDisplay.Id,
                    NumeroEtape = etapeToDisplay.NumeroEtape,
                    Nom = etapeToDisplay.Nom,
                    ArticlesConsommes = etapeToDisplay.ArticlesConsommes,
                    DureePrevue = etapeToDisplay.DureePrevue,
                    Start = DateTime.Now,
                };

                OrdreFabrication.EtapesExecuted.Add(executedStep);
                DisplayInformations();

                await OrdreFabricationProvider.UpdateAsync(OrdreFabrication);
            }

            await InvokeAsync(StateHasChanged);
        }
        private async Task EndStep(Etape etapeToDisplay)
        {
            var etapeExecuted = OrdreFabrication.EtapesExecuted.FirstOrDefault(v => v.Id == etapeToDisplay.Id);
            etapeExecuted.End = DateTime.Now;

            var index = OrdreFabrication.EtapesExecuted.FindIndex(v => v.Id == etapeToDisplay.Id);
            OrdreFabrication.EtapesExecuted[index] = etapeExecuted;

            OrdreFabrication = await OrdreFabricationProvider.UpdateAsync(OrdreFabrication);
            
            foreach (var articleConsomme in etapeExecuted.ArticlesConsommes)
            {
                var article = await ArticleProvider.GetOneSimpleAsync(articleConsomme.ArticleId);
                article.StockReserved -= articleConsomme.QuantityToUse;
                article.Stock -= articleConsomme.QuantityUsed;
                await ArticleProvider.UpdateAsync(article);
            }

            if (index + 1 < OrdreFabrication.Gamme.Etapes.Count)
            {
                EtapeToDisplay = OrdreFabrication.Gamme.Etapes[index + 1];
                await DisplayStep(EtapeToDisplay);
            }

            DisplayInformations();
            await InvokeAsync(StateHasChanged);
        }
    }
}