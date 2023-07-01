using Blazor.Provider.Api.CallApiProvider;
using Common.Models.Business;
using Microsoft.Extensions.Options;

namespace Blazor.Provider.Api
{
    public class ChildProvider : BaseCallApi<Child>
    {
        public ChildProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}
