using Blazor.Controller.Modal;
using Blazor.Provider.Api.CallApiProviderBase;
using Blazor.Provider.Api.MES;
using Blazored.Modal;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class ManageArticle
    {
        [Inject]
        public ICallApi<Article> ArticleProvider { get; set; }

        [Inject]
        public ICallApi<Gamme> GammeProvider { get; set; }

        [Inject]
        public ICallApi<OrdreFabrication> OrdreFabricationProvider { get; set; }

        [Inject]
        public ModalController? ModalController { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public List<Article> Articles { get; set; }
        public List<Gamme> Gammes { get; set; }

        public Article? Article { get; set; } = new Article();
        public Gamme? Gamme { get; set; } = new Gamme();
        public Etape? Etape { get; set; } = new Etape();

        protected override async Task OnInitializedAsync()
        {
            var articles = await ArticleProvider.GetAllAsync();
            Articles = articles.ToList();
        }
        private async Task ReloadFullArticle(string? articleId)
        {
            if (!string.IsNullOrEmpty(articleId))
            {
                Article = await ArticleProvider.GetOneFullAsync(articleId);
                Gammes = Article.GammesFabrication;

                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnInitializedAsync();
            }
        }

        private async Task AddNewArticle()
        {
            Article = new Article();
            await InvokeAsync(StateHasChanged);
        }

        private async Task SelectArticle(ChangeEventArgs arg)
        {
            if (!string.IsNullOrWhiteSpace(arg.Value.ToString()))
            {
                Article = Articles.FirstOrDefault(v => v.Id == arg.Value.ToString());
                if (Article.EstFabrique)
                {
                    Article = await ArticleProvider.GetOneFullAsync(arg.Value.ToString());
                    Gammes = Article.GammesFabrication;
                }
                else
                {
                    Gammes = null;
                }
            }
            else
            {
                Article = new Article();
                Gammes = null;
            }

            Gamme = new Gamme(); 

            await InvokeAsync(StateHasChanged);
        }
        private async Task AddNewGamme()
        {
            if (Article.EstFabrique)
            {
                Gamme = new Gamme() { ArticleId = Article.Id, Etapes = new List<Etape>() };
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task SelectGamme(ChangeEventArgs arg)
        {
            if (!string.IsNullOrWhiteSpace(arg.Value.ToString()))
            {
                Gamme = await GammeProvider.GetOneFullAsync(arg.Value.ToString()); 
            }
            else
            {
                Gamme = new Gamme() { ArticleId = Article.Id, Etapes = new List<Etape>() };
            }

            _zones = Gamme.Etapes
                .DistinctBy(v => v.DropZone)
                .Select(v => v.DropZone)
                .Order()
                .ToList();

            await InvokeAsync(StateHasChanged);
        }

        private List<string> _zones { get; set; }


        private async Task AddNewStep()
        {
            var parameters = new ModalParameters()
            {
                {nameof(_EtapeModal.GammeId), Gamme.Id}
            }; 
            var result = (Etape)await ModalController.ShowModal<_EtapeModal>($"Ajouter une étape", parameters); 

            Gamme.Etapes.Add(result);
            await InvokeAsync(StateHasChanged); 
        }

        private async Task GoProduction()
        {
            var of = new OrdreFabrication()
            {
                GammeId = Gamme.Id, 
                Gamme = Gamme
            };
            of = await OrdreFabricationProvider.CreateAsync(of);

            NavigationManager.NavigateTo($"/productionEnCours/{of.Id}");
        }

        private void Do()
        {
            foreach (var etape in Gamme.Etapes)
            {
                
            }
        }
    }
}
