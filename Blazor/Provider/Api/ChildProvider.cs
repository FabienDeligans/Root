using Common.Models.Business;
using Front.BlazorExceptionManager;
using Front.CallApiProvider;
using Microsoft.Extensions.Options;

namespace Blazor.Provider.Api
{
    public class ChildProvider : BaseCallApi<Child>
    {
        public ChildProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}
