using Blazor.Provider.Api.CallApiProviderBase;
using Blazored.Modal;
using Blazored.Modal.Services;
using Common.Models.MES;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MES
{
    public partial class _EtapeModal
    {
        [Inject]
        public ICallApi<Etape> EtapeProvider { get; set; }

        [Inject]
        public ICallApi<Article> ArticleProvider { get; set; }

        [Inject]
        public ICallApi<GammeEtape> GammeEtapeProvider { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance? BlazoredModal { get; set; }

        [Parameter]
        public string GammeId { get; set; }

        [Parameter]
        public string EtapeId { get; set; }

        public Etape Etape { get; set; } = new Etape();

        public Article Article { get; set; }

        private TimeSpan? duration;

        public IEnumerable<Article> Articles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var articleTask = ArticleProvider.GetAllAsync();

            if (string.IsNullOrEmpty(EtapeId))
            {
                Etape = new Etape() { ArticlesConsommes = new List<ArticleConsome>() };
            }
            else
            {
                Etape = await EtapeProvider.GetOneSimpleAsync(EtapeId);
            }

            Articles = await articleTask;
        }


        private void SelectArticle(ChangeEventArgs obj)
        {
            if (!string.IsNullOrEmpty(obj.Value.ToString()))
            {
                Article = Articles.FirstOrDefault(v => v.Id == obj.Value.ToString());
            }
        }

        private async Task AddNewArticleToConsume()
        {
            Etape.ArticlesConsommes.Add(new ArticleConsome()
            {
                ArticleId = Article.Id,
                ArticleNom = Article.Nom,
                QuantityToUse = 0,
            });
            await InvokeAsync(StateHasChanged);
        }

        private async Task Save()
        {
            Etape.DureePrevue = duration.Value;
            if (string.IsNullOrEmpty(Etape.Id))
            {
                Etape = await EtapeProvider.CreateAsync(Etape);
                await GammeEtapeProvider.CreateAsync(new GammeEtape()
                {
                    EtapeId = Etape.Id,
                    GammeId = GammeId,
                }); 
            }
            else
            {
                Etape = await EtapeProvider.UpdateAsync(Etape);
                
            }


            await InvokeAsync(async () => await BlazoredModal.CloseAsync(ModalResult.Ok(Etape)));
        }

        private Task Reset()
        {
            throw new NotImplementedException();
        }
    }
}
