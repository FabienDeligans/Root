using Blazor.Provider.Api.MES;
using Common.Helper;
using Common.Models.MES;
using Common.Models.MES.Article;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class ArticlePage
    {
        [Inject]
        public ManufacturedArticleProvider ManufacturedArticleProvider { get; set; }

        [Inject]
        public PurchasedArticleProvider PurchasedArticleProvider { get; set; }

        [Inject]
        public OpeProvider OpeProvider { get; set; }

        [Inject]
        public GammeProvider GammeProvider { get; set; }

        public List<ManufacturedArticle>? ManufacturedArticles { get; set; } = new List<ManufacturedArticle>();
        public List<PurchasedArticle>? PurchasedArticles { get; set; } = new List<PurchasedArticle>();
        public int? Nb { get; set; } = 10;

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task GeneratePurchasedArticles()
        {
            PurchasedArticles = new List<PurchasedArticle>();

            for (var i = 0; i < Nb; i++)
            {
                var article = new PurchasedArticle()
                {
                    Name = $"{RandomHelper.GetRandomString(10)} - {i}",
                    Quantity = 10
                };

                PurchasedArticles.Add(await PurchasedArticleProvider.CreateAsync(article));
            }
        }

        public async Task GenerateManufacturedArticles()
        {
            for (var i = 0; i < Nb; i++)
            {
                var manufacturedArticle = new ManufacturedArticle()
                {
                    Name = $"Article Name-{i}",
                    Quantity = 1,
                    Version = "1.0.0"
                };
                manufacturedArticle = await ManufacturedArticleProvider.CreateAsync(manufacturedArticle); 

                var gamme = new Gamme
                {
                    ManufacturedArticleId = manufacturedArticle.Id,
                    OpeGamme = new List<Ope>()
                };

                var opes = new List<Ope>();
                for (var j = 0; j < 5; j++)
                {
                    var ope = new Ope
                    {
                        Step = j,
                        Name = $"ope-{manufacturedArticle.Id}-{j}",
                        Description = $"ope description {j}",
                    };
                    ope = await OpeProvider.CreateAsync(ope);
                    opes.Add(ope);
                }
                gamme.OpeGamme.AddRange(opes);

                await GammeProvider.CreateAsync(gamme);
            }

            await OnInitializedAsync();
            await InvokeAsync(StateHasChanged);
        }

        public async Task DropMES()
        {
            //await OpeProvider.DropCollectionAsync();

            //await OnInitializedAsync();
            //await InvokeAsync(StateHasChanged);
        }
    }
}
