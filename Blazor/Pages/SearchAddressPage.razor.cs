using Library.Blazor.CallApiAddressProvider;
using Library.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class SearchAddressPage
    {
        [Inject]
        public ApiAddressProvider ApiAddressProvider { get; set; }

        public AddressResult? Address { get; set; }
        private async Task Search(ChangeEventArgs arg)
        {
            var value = arg.Value.ToString();
            Address = await ApiAddressProvider.GetListAddress(value, 100);
        }
    }
}
