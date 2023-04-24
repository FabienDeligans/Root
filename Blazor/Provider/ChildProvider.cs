using Library.Abstract;
using Library.Models.Business;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class ChildProvider : BaseCallApi<Child>
    {
        public ChildProvider(HttpClient client, IOptions<SettingsCallApi> options) : base(client, options)
        {
        }
    }
}
