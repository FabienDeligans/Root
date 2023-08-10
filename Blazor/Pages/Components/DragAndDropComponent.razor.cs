using Common.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace Blazor.Pages.Components
{
    public partial class DragAndDropComponent<T> : ChildComponentBase where T : IOrderItem
    {
        [CascadingParameter]
        public List<T> BaseListOf { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string? DropZoneTitle { get; set; }

        private MudDropContainer<T> _container;

        private List<T> _dropzoneItems = new();

        protected override void OnInitialized()
        {
            
        }

        private void LoadData()
        {
            _dropzoneItems = BaseListOf;
            RefreshContainer();
        }

        protected void RefreshContainer()
        {
            StateHasChanged();
            _container.Refresh();
        }


        private async Task ItemUpdated(MudItemDropInfo<T> dropItem)
        {
            BaseListOf.UpdateOrder(dropItem, item => item.Order);
            await RefreshParent(); 
        }
    }
}
