using Library.Blazor.BlazorExceptionManager;
using Library.Blazor.CallApiProvider;
using Library.Models.Business;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class FamilyProvider : BaseCallApi<Family>
    {
        public FamilyProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}