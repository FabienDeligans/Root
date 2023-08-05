using Blazor.Controller.Modal;
using Blazor.Provider.Api.CallApiProviderBase;
using Common.Helper;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class ArticlePage
    {
        [Inject]
        public ICallApi<Article> ArticleProvider { get; set; }

        [Inject]
        public ICallApi<Etape> EtapeProvider { get; set; }

        [Inject]
        public ICallApi<GammeEtape> GammeEtapeProvider { get; set; }

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
        public List<Etape> Etapes { get; set; }
        public List<GammeEtape> GammeEtapes { get; set; }
        public List<OrdreFabrication> OrdreFabrications { get; set; }

        private string gammeSelectedId;

        protected override async Task OnInitializedAsync()
        {
            await ModalController.ShowSpinner(async () =>
            {
                Articles = new List<Article>();
                Gammes = new List<Gamme>();
                Etapes = new List<Etape>();
                GammeEtapes = new List<GammeEtape>();

                Articles = ArticleProvider.GetAllAsync().Result.ToList();
                Gammes = GammeProvider.GetAllAsync().Result.ToList();
                Etapes = EtapeProvider.GetAllAsync().Result.ToList();
                GammeEtapes = GammeEtapeProvider.GetAllAsync().Result.ToList();

                var ofs = await OrdreFabricationProvider.GetAllAsync();
                if (ofs != null)
                {
                    OrdreFabrications = ofs.ToList();
                }

                if (!Articles.Any())
                {
                    await DropMES();
                    await GenerateArticle();
                    await GenerateGamme();
                }

            });
        }

        public async Task DropMES()
        {
            await ModalController.ShowSpinner(async () =>
            {
                Articles = new List<Article>();
                Gammes = new List<Gamme>();
                Etapes = new List<Etape>();
                GammeEtapes = new List<GammeEtape>();

                await ArticleProvider.DropCollectionAsync();
                await GammeProvider.DropCollectionAsync();
                await EtapeProvider.DropCollectionAsync();
                await GammeEtapeProvider.DropCollectionAsync();
                await OrdreFabricationProvider.DropCollectionAsync();
            });
        }

        #region Generation SoumSoum

        public async Task<GammeEtape> LinkGammeEtape(Gamme gamme, Etape etape)
        {
            var gammeEtape = new GammeEtape
            {
                GammeId = gamme.Id,
                EtapeId = etape.Id,
            };
            gammeEtape = await GammeEtapeProvider.CreateAsync(gammeEtape);
            GammeEtapes.Add(gammeEtape);
            return gammeEtape;
        }

        public async Task<Etape> GenerateEtape(Gamme gamme, int numEtape)
        {
            var etape = new Etape
            {
                NumeroEtape = numEtape,
                Nom = $"etape n° {numEtape} de - {gamme.Nom}",
                ArticlesConsommes = new List<ArticleConsome>()
            };

            for (var i = 0; i < new Random().Next(1,5); i++)
            {
                var randomArticle = Articles[new Random().Next(1,Articles.Count)];
                var articleConsome = new ArticleConsome
                {
                    ArticleId = randomArticle.Id,
                    ArticleNom = randomArticle.Nom,
                    QuantityToUse = new Random().Next(1, 3)
                };

                etape.ArticlesConsommes.Add(articleConsome);
            }
            etape = await EtapeProvider.CreateAsync(etape);
            Etapes.Add(etape);
            return etape;
        }

        #endregion

        public async Task GenerateGamme()
        {
            await ModalController.ShowSpinner(async () =>
            {
                foreach (var article in Articles.Where(v => v.EstFabrique == true))
                {
                    for (var j = 0; j < 5; j++)
                    {
                        var gamme = new Gamme
                        {
                            ArticleId = article.Id,
                            Nom = $"Gamme - n° {j} de - {article.Nom} ",
                            Etapes = new List<Etape>()
                        };
                        gamme = await GammeProvider.CreateAsync(gamme);
                        Gammes.Add(gamme);

                        for (var i = 0; i < new Random().Next(1, 10); i++)
                        {
                            var etape = await GenerateEtape(gamme, i);
                            var gammeEtape = await LinkGammeEtape(gamme, etape);
                        }
                    }
                }
            });
        }

        public async Task GenerateArticle()
        {
            await ModalController.ShowSpinner(async () =>
            {
                Articles = new List<Article>();

                for (var i = 0; i < 10; i++)
                {
                    var article = new Article
                    {
                        Nom = $"{RandomHelper.GetRandomString(10)}",
                        EstFabrique = false,
                        GammesFabrication = null,
                        Stock = 10
                    };
                    Articles.Add(await ArticleProvider.CreateAsync(article));
                }

                for (var i = 0; i < 10; i++)
                {
                    var article = new Article
                    {
                        Nom = $"{RandomHelper.GetRandomString(10)}",
                        EstFabrique = true,
                        GammesFabrication = null,
                        Stock = 10
                    };
                    Articles.Add(await ArticleProvider.CreateAsync(article));
                }
            });
        }

        private void GoProduction(string gammeId)
        {
            NavigationManager.NavigateTo($"/production/{gammeId}");
        }
    }
}
