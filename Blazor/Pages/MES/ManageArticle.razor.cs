using Blazor.Provider.Api.CallApiProviderBase;
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

        public List<Article> Articles { get; set; }
        public List<Gamme> Gammes { get; set; }

        public Article? Article { get; set; } = new Article();
        public Gamme? Gamme { get; set; } = new Gamme();
        public Etape? Etape { get; set; } = new Etape();

        private string _statusMessage;
        private string _statusClass;

        protected override async Task OnInitializedAsync()
        {
            var articles = await ArticleProvider.GetAllAsync();
            Articles = articles.ToList();
        }

        private async Task Cancel()
        {
            Article = new Article();
            Gamme = null;
            Etape = null;
            await InvokeAsync(StateHasChanged);
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
                    var gammes = await GammeProvider.GetAllFilteredByPropertyEqualAsync(nameof(Gamme.ArticleId), Article.Id);
                    Gammes = gammes.ToList();
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

            await InvokeAsync(StateHasChanged);
        }

        private async Task SelectGamme(ChangeEventArgs arg)
        {
            if (!string.IsNullOrWhiteSpace(arg.Value.ToString()))
            {
                Gamme = Gammes.FirstOrDefault(v => v.Id == arg.Value.ToString());
            }
            else
            {
                Gamme = new Gamme() { ArticleId = Article.Id, Etapes = new List<Etape>() };
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task AddNewGamme()
        {
            if (Article.EstFabrique == true)
            {
                Gamme = new Gamme() { ArticleId = Article.Id, Etapes = new List<Etape>() };
            }

            await InvokeAsync(StateHasChanged);
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

            var gammes = await GammeProvider.GetAllFilteredByPropertyEqualAsync(nameof(Gamme.ArticleId), Article.Id);
            Gammes = gammes.ToList();
            await InvokeAsync(StateHasChanged);
        }
    }
}
