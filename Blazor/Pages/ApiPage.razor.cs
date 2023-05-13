using Library.Blazor.CallApiAddressProvider;
using Library.Blazor.CallApiLoraineProvider;
using Library.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class ApiPage
    {
        [Inject]
        public ApiAddressProvider ApiAddressProvider { get; set; }
        public AddressResult? Address { get; set; }

        private async Task Search(ChangeEventArgs arg)
        {
            var value = arg.Value.ToString();
            Address = await ApiAddressProvider.GetListAddress(value, 10);
            await InvokeAsync(StateHasChanged); 
        }


        [Inject]
        public LorraineHipseaumeProvider LorraineHipseameProvider { get; set; }
        public List<NomPrenomHipseaume> ListNomPrenomHipseaume { get; set; }

        private async Task SearchNameLorraineHipseaume()
        {
            ListNomPrenomHipseaume = await LorraineHipseameProvider.GetListRandomName();
            await InvokeAsync(StateHasChanged);
        }


        [Inject]
        public LorraineIpsumProvider LorraineIpsumProvider { get; set; }
        public IEnumerable<NomPrenomIpsum> ListNomPrenomIpsum { get; set; }

        private async Task SearchNameLorraineIpsum()
        {
            ListNomPrenomIpsum = await LorraineIpsumProvider.GetListRandomName(); 
            await InvokeAsync(StateHasChanged);
        }

    }
}
