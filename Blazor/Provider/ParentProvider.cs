using Library.Abstract;
using Library.Models.Business;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class ParentProvider : BaseCallApi<Parent>
    {
        public ParentProvider(HttpClient client, IOptions<SettingsCallApi> options) : base(client, options)
        {
        }
    }
}
