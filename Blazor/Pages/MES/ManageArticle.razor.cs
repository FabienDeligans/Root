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

        private string _statusMessage;
        private string _statusClass;

        protected override async Task OnInitializedAsync()
        {
            var articles = await ArticleProvider.GetAllAsync();
            Articles = articles.ToList();
        }

        private async Task AddNewArticle()
        {
            Article = new Article();
        }

        private async Task SelectArticle(ChangeEventArgs arg)
        {
            Article = Articles.FirstOrDefault(v => v.Id == arg.Value.ToString());
            if (Article.EstFabrique)
            {
                var gammes = await GammeProvider.GetAllAsync();
                Gammes = gammes.Where(v => v.ArticleId == Article.Id).ToList();
            }
            else
            {
                Gammes = new List<Gamme>(); 
            }
        }

        private async Task Submit()
        {
            if (Article.Id != null)
            {
                Article = await ArticleProvider!.UpdateAsync(Article);
            }
            else
            {
                Article = await ArticleProvider!.CreateAsync(Article);
            }

            await OnInitializedAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task Cancel()
        {
            Article = new Article();
            await InvokeAsync(StateHasChanged);
        }

        private async Task SelectGamme(ChangeEventArgs arg)
        {
            Gamme = Gammes.FirstOrDefault(v => v.Id == arg.Value.ToString());
        }
    }
}
