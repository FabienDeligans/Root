using Blazor.Controller.Modal;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class ModalPage
    {
        [Inject]
        public ModalController ModalController { get; set; }

        private async Task ShowModal()
        {
            await ModalController.ShowModal<Index>();
        }

        private async Task ShowModalDuration()
        {
            await ModalController.ShowModalDuration(Lorem, 5000, Alert.Primary);
        }

        private string Lorem { get; set; } =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

    }
}
