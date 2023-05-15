using Library.Blazor.CallApiProvider;
using Library.Blazor.ExceptionManager;
using Library.Models.Business;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class ParentProvider : BaseCallApi<Parent>
    {
        public ParentProvider(
            HttpClient client, 
            IOptions<SettingsCallApi> options,
            ExceptionManager exceptionManager) 
            : base(client, options, exceptionManager)
        {
        }
    }
}
