using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.MES;
using Microsoft.Extensions.Options;

namespace Blazor.Provider.Api.MES;

public class ArticleProvider : BaseCallApi<Article>
{
    public ArticleProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
    {
    }
}