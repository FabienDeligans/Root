using Blazor.Pages.Components;
using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;
using static Azure.Core.HttpHeader;

namespace Blazor.Pages.MES
{
    public partial class _ArticleComponent : ChildComponentBase
    {
        [Inject]
        public ICallApi<Article>? ArticleProvider { get; set; }

        [Inject]
        public ICallApi<Gamme>? GammeProvider { get; set; }

        [CascadingParameter]
        public Article Article { get; set; }

        public Gamme Gamme { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Article.Id))
            {
                Article = await ArticleProvider.GetOneSimpleAsync(Article.Id); 
            }
            else
            {
                Article = new Article();
            }
        }
        
        private async Task SubmitArticle()
        {
            if (!string.IsNullOrEmpty(Article.Id))
            {
                Article = await ArticleProvider.UpdateAsync(Article);
            }
            else
            {
                Article = await ArticleProvider.CreateAsync(Article);
            }

            await RefreshParent(); 
            await OnInitializedAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task Cancel()
        {
            Article = new Article();
            await InvokeAsync(StateHasChanged);
        }
    }
}
