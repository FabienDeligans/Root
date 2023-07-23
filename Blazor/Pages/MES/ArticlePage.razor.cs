using Blazor.Provider.Api.MES;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;

namespace Blazor.Pages.MES
{
    public partial class ArticlePage
    {
        [Inject]
        public ManufacturedArticle ManufacturedArticle { get; set; }

        [Inject]
        public PurchasedArticle PurchasedArticle { get; set; }

        [Inject]
        public OpeProvider OpeProvider { get; set; }

        [Inject]
        public GammeProvider GammeProvider { get; set; }

        public List<ManufacturedArticle>? ManufacturedArticles { get; set; }
        public List<PurchasedArticle>? PurchasedArticles { get; set; }
        public int? Nb { get; set; } = 10;

        protected override async Task OnInitializedAsync()
        {
            
        }

        public async Task GenerateManufacturedArticles()
        {
            //for (var i = 0; i < Nb; i++)
            //{
            //    var article = new ManufacturedArticle()
            //    {
            //        Name = $"Article Name-{i}",
            //        Quantity = 1,
            //        TypeArticle = TypeArticle.Manufactured,
            //        Version = "1.0.0"
            //    };

            //    var gamme = new Gamme();
            //    var opes = new List<Ope>();
            //    for (var j = 0; j < 5; j++)
            //    {
            //        var ope = new Ope
            //        {
            //            Step = j,
            //            Name = $"ope-{article.Id}-{j}",
            //            Description = $"ope description {j}",
            //            ArticleId = article.Id,
            //        };
            //        ope = await OpeProvider.CreateAsync(ope); 
            //        opes.Add(ope);
            //    }
            //    gamme = await GammeProvider.CreateAsync(gamme); 

            //    article.GammeId = gamme.Id;
            //}

            //await OnInitializedAsync();
            //await InvokeAsync(StateHasChanged); 
        }

        public async Task DropMES()
        {
            //await OpeProvider.DropCollectionAsync();

            //await OnInitializedAsync();
            //await InvokeAsync(StateHasChanged);
        }
    }
}
