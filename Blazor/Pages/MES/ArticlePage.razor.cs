using Blazor.Provider.Api.MES;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;

namespace Blazor.Pages.MES
{
    public partial class ArticlePage
    {
        [Inject]
        public ArticleProvider ArticleProvider { get; set; }

        [Inject]
        public OpeProvider OpeProvider { get; set; }

        public List<Article>? Articles { get; set; } = new List<Article>();
        public int? Nb { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Articles = ArticleProvider.GetAllAsync().Result.ToList(); 
        }

        public async Task GenerateArticles()
        {
            for (var i = 0; i < Nb; i++)
            {
                var article = new Article
                {
                    Name = $"Article Name-{i}",
                    Quantity = 1,
                    TypeArticle = TypeArticle.Manufactured,
                };

                article = await ArticleProvider.CreateAsync(article); 

                var opes = new List<Ope>();
                for (var j = 0; j < 5; j++)
                {
                    var ope = new Ope
                    {
                        Step = j,
                        Name = $"ope-{article.Id}-{j}",
                        Description = $"ope description {j}",
                        ArticleId = article.Id,
                    };
                    ope = await OpeProvider.CreateAsync(ope); 
                    opes.Add(ope);
                }

                article.Operations = opes;

                Articles.Add(article); 
            }

            await InvokeAsync(StateHasChanged); 
        }
    }
}
