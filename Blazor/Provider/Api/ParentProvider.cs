using Blazor.Provider.Api.CallApiProviderBase;
using Common.Models.Business;
using Microsoft.Extensions.Options;

namespace Blazor.Provider.Api
{
    public class ParentProvider : BaseCallApi<Parent>
    {
        public ParentProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}
