using Library.Blazor.BlazorExceptionManager;
using Library.Blazor.CallApiProvider;
using Library.Models.Process;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class ProcessProvider : BaseCallApi<ProcessModel>
    {
        public ProcessProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}
