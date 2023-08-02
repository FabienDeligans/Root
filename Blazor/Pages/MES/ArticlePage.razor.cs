using Blazor.Provider.Api.MES;
using Common.Helper;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class ArticlePage
    {
        [Inject]
        public ArticleProvider ArticleProvider { get; set; }

        [Inject]
        public EtapeProvider EtapeProvider { get; set; }

        [Inject]
        public GammeEtapeProvider GammeEtapeProvider { get; set; }

        [Inject]
        public GammeProvider GammeProvider { get; set; }

        [Inject]
        public OrdreFabricationProvider OrdreFabricationProvider { get; set; }

        public async Task GenerateEtape()
        {

            var etape = new Etape
            {
                NumeroEtape = 0,
                Nom = null,
                ArticlesConsommes = new List<ArticleConsome>
                {
                    new ArticleConsome
                    {
                        ArticleId = null,
                        ArticleNom = null,
                        QuantityToUse = 0
                    },
                },
            };
        }

        public async Task GeneratePurchasedArticle()
        {
            for (var i = 0; i < 10; i++)
            {
                var article = new Article
                {
                    Nom = $"{RandomHelper.GetRandomString(5)} - {i}",
                    EstFabrique = false,
                    GammesFabrication = null,
                    Stock = 10
                };
                await ArticleProvider.CreateAsync(article); 
            }
        }
    }
}
