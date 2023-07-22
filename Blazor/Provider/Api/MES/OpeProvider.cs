using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.MES;
using Microsoft.Extensions.Options;

namespace Blazor.Provider.Api.MES;

public class OpeProvider : BaseCallApi<Ope>
{
    public OpeProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
    {
    }
}