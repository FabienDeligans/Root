using Common.Models.Business;
using Front.BlazorExceptionManager;
using Front.CallApiProvider;
using Microsoft.Extensions.Options;

namespace Blazor.Provider.Api
{
    public class FamilyProvider : BaseCallApi<Family>
    {
        public FamilyProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}